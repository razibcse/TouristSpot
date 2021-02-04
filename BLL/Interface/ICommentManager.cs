using BLL.Base;
using Models;
using Models.CreateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface ICommentManager:IManager<Comment>
    {
        public bool AddComment(AddComment model);
    }
}
