using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDesciption { get; set; }

        public string Description { get; set; }

        public string MainImage { get; set; }

        public string[] DetailedOtherImages { get; set; }

        public ICollection<BlogImage> BlogImages { get; set; }

        public ICollection<BlogLike> BlogLikes { get; set; }

        public ICollection<BlogUnlike> BlogUnlikes { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }

        public int CommentCount
        {
            get
            {
                if (BlogComments.Where(bc => bc.ParentId == null).Count() > 0)
                    return BlogComments.Where(bc => bc.ParentId == null).Count();

                return 0;
            }
        }

        public int LikeCount
        {
            get
            {
                if (BlogLikes.Count > 0)
                    return BlogLikes.Count;

                return 0;
            }
        }

        public int UnlikeCount
        {
            get
            {
                if (BlogUnlikes.Count > 0)
                    return BlogUnlikes.Count;

                return 0;
            }
        }

        public string Author { get; set; }
    }
}
