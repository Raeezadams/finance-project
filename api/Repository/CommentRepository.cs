using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _dbContext.Comments.AddAsync(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            
            if(commentModel == null)
            {
                return null;
            }

            _dbContext.Comments.Remove(commentModel);
            await _dbContext.SaveChangesAsync();

            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _dbContext.Comments.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var exsistingComment = await _dbContext.Comments.FindAsync(id);
            
            if (exsistingComment == null)
            {
                return null;
            }

            exsistingComment.Title = commentModel.Title;
            exsistingComment.Content = commentModel.Content;

            await _dbContext.SaveChangesAsync();
            return exsistingComment;
        }


    }
}
