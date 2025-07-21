namespace BlogBuilder.DTOs
{
    public class CommentsDTO
    {
        public int COMMENTID { get; set; }

        public int USERID { get; set; }

        public int BLOGID { get; set; }

        public string COMMENT { get; set; }
        public DateOnly? MODIFIED_DATE { get; set; }
    }
}
