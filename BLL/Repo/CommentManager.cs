using BLL.Base;
using BLL.Interface;
using Models;
using Models.CreateViewModels;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class CommentManager : Manager<Comment>, ICommentManager
    {
        ICommentRepository repository;
        public CommentManager(ICommentRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public bool AddComment(AddComment model)
        {
            return repository.AddComment(model);
        }
    }
}

