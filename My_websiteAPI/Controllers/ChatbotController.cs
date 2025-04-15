using Microsoft.AspNetCore.Mvc;
using My_websiteAPI.Model;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using My_websiteAPI.Data;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Text.Json;
using System.Text;

namespace My_websiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly ElasticsearchClient _client;
        private readonly MyDBcontext _context;

        // Khởi tạo client ElasticSearch và context của cơ sở dữ liệu thông qua Dependency Injection
        public ChatBotController(MyDBcontext context, ElasticsearchClient client)
        {
            _context = context;
            _client = client;
        }

        // Phương thức để nhận câu hỏi và thực hiện tìm kiếm trong Elasticsearch và CSDL
        [HttpPost]
        public async Task<IActionResult> Chat(Usersend question)
        {
            // Tìm kiếm trong Elasticsearch
            var elasticResults = await SearchElasticAsync(question.query);

            // Nếu tìm thấy kết quả trong Elasticsearch, trả về kết quả
            if (elasticResults.Any())
            {
                // Ghép câu hỏi và dữ liệu tìm được
                string elasticInfo = string.Join("\n", elasticResults.Select(r => r.Content));
                string userPrompt = $"Người dùng hỏi: {question}\n\nThông tin bạn tìm được từ dữ liệu:\n{elasticInfo}\n\nHãy dựa vào thông tin trên và trả lời thật tự nhiên, rõ ràng. Nếu có thể hãy tư vẫn thêm thông tin và lưu ý.";

                // Gửi đến LM Studio (local API)
                using var httpClient = new HttpClient();
                var payload = new
                {
                    model = "llama-3.2-3b-instruct-frog", // hoặc model bạn chọn
                    messages = new[]
                    {
                        new { role = "system", content = "Bạn là trợ lý tư vấn du lịch và ẩm thực tại Việt Nam." },
                        new { role = "user", content = userPrompt }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:1234/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(resultJson);
                    string aiAnswer = doc.RootElement
                                         .GetProperty("choices")[0]
                                         .GetProperty("message")
                                         .GetProperty("content")
                                         .GetString();

                    return Ok(new { answer = aiAnswer });
                }
                else
                {
                    return StatusCode(500, new { error = "Không thể kết nối đến LM Studio." });
                }
            }
            else
            {
               
                string userPrompt = $"Người dùng hỏi: {question}\n\n Bạn hãy trả lời thật tự nhiên, rõ ràng. Nếu có thể hãy tư vẫn thêm thông tin và lưu ý.";

                // Gửi đến LM Studio (local API)
                using var httpClient = new HttpClient();
                var payload = new
                {
                    model = "llama-3.2-3b-instruct-frog", // hoặc model bạn chọn
                    messages = new[]
                    {
                        new { role = "system", content = "Bạn là trợ lý tư vấn du lịch và ẩm thực tại Việt Nam." },
                        new { role = "user", content = userPrompt }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:1234/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(resultJson);
                    string aiAnswer = doc.RootElement
                                         .GetProperty("choices")[0]
                                         .GetProperty("message")
                                         .GetProperty("content")
                                         .GetString();

                    return Ok(new { answer = aiAnswer });
                }
                else
                {
                    return StatusCode(500, new { error = "Không thể kết nối đến LM Studio." });
                }

            }

          
          

            return Ok(new { answer = "Không tìm thấy kết quả cho câu hỏi của bạn." });
        }

        // Tìm kiếm trong Elasticsearch
        private async Task<List<ElasticResult>> SearchElasticAsync(string query)
        {
            try
            {
                var searchResponse = await _client.SearchAsync<ElasticResult>(s => s
                    .Index("index_dd")  // Tên chỉ mục Elasticsearch của bạn
                    .Size(1) // Giới hạn số lượng kết quả trả về, có thể điều chỉnh tùy vào nhu cầu
                    .Query(q => q
                        .Bool(b => b
                            .Must(m => m.Match(match => match.Field(f => f.Content).Query(query))) 
                        )
                    )
                );

                // Kiểm tra nếu phản hồi hợp lệ và có kết quả
                if (searchResponse.IsValidResponse && searchResponse.Hits.Count > 0)
                {
                    // Lấy các kết quả từ Hits và chuyển đổi thành danh sách ElasticResult
                    return searchResponse.Hits.Select(hit => hit.Source).ToList();
                }

                // Nếu không có kết quả hoặc có lỗi, trả về danh sách trống
                return new List<ElasticResult>();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ví dụ: log lỗi
                Console.Error.WriteLine($"Error occurred while searching: {ex.Message}");
                return new List<ElasticResult>();
            }
        }


        // Tìm kiếm trong cơ sở dữ liệu
        private async Task<List<string>> SearchDatabaseAsync(string query)
        {
            var diadiemList = await _context.Diadiem
                .Where(d => EF.Functions.Like(d.Tieude, $"%{query}%") || EF.Functions.Like(d.Noidung, $"%{query}%"))
                .ToListAsync();

            return diadiemList.Select(d => $"{d.Tieude} - {d.Noidung}").ToList();
        }

        // Phương thức đẩy dữ liệu vào Elasticsearch
        [HttpPost("daydl")]
        public async Task<IActionResult> Daydl()
        {
            var diadiemList = await _context.Diadiem.Include(p=>p.LoaiHinhDL).Include(p => p.TinhThanh).Include(p => p.Danhcho).ToListAsync();
            int insertedCount = 0;

            foreach (var item in diadiemList)
            {
                var docId = item.DiadiemId.ToString();

                // Kiểm tra document đã tồn tại chưa
                var existsResponse = await _client.ExistsAsync<ElasticResult>(docId, e => e.Index("index_dd"));
                if (!existsResponse.Exists)
                {
                    var doc = new ElasticResult
                    {
                        Content =
                                $"Tiêu đề: {item.Tieude}\n" +
                                $"Giới thiệu: {item.Noidung}\n" +
                                $"Địa chỉ: {item.Diachi}\n" +
                                $"Loại: {item.LoaiHinhDL.TenLoai}\n" +
                                $"Lượt xem trên web là: {item.Luotxem}\n" +
                                $"Đối tượng tham gia: {item.Danhcho.Doituong}\n" +
                                $"Địa chỉ nằm trong tỉnh: {item.TinhThanh.TenTinh}"


                    };

                    await _client.IndexAsync(doc, idx => idx.Index("index_dd").Id(docId));
                    insertedCount++;
                }
            }

            return Ok(new { message = $"Đã đẩy {insertedCount} địa điểm mới lên Elasticsearch." });
        }

        [HttpDelete("xoatatca")]
        public async Task<IActionResult> DeleteAllDocuments()
        {
            var request = new DeleteByQueryRequest("index_dd")
            {
                Query = new MatchAllQuery()
            };

            var response = await _client.DeleteByQueryAsync(request);

            if (response.IsValidResponse)
            {
                return Ok(new { message = $"Đã xoá {response.Deleted} tài liệu trong Elasticsearch." });
            }
            else
            {
                return StatusCode(500, new
                {
                    error = "Xoá dữ liệu thất bại."
                
                });
            }
        }



    }
}
