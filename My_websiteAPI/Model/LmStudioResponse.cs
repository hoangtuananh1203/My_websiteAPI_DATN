namespace My_websiteAPI.Model
{
    public class LmStudioResponse
    {
        public List<Choice> Choices { get; set; }

        public class Choice
        {
            public Message Message { get; set; }
        }

        public class Message
        {
            public string Content { get; set; }
        }
    }

}
