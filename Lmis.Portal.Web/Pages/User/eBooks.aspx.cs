﻿using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class eBooks : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillEBooks();
        }

        private void FillEBooks()
        {
            var entities = (from n in DataContext.LP_EBooks
                            where n.DateDeleted == null
                            orderby n.DateCreated descending
                            select n).ToList();

            var converter = new EBookEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new EBooksModel();
            model.List = models;

            eBooksControl.Model = model;
        }
    }
}