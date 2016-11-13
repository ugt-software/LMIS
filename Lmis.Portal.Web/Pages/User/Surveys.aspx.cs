﻿using System;
using System.Linq;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class Surveys : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillSurveys();
        }

        private void FillSurveys()
        {
            var parentID = DataConverter.ToNullableGuid(Request["ID"]);

            var currentLanguage = LanguageUtil.GetLanguage();

            var query = from n in DataContext.LP_Surveys
                        where n.DateDeleted == null && (n.Language == currentLanguage || n.Language == null || n.Language == "")
                        select n;

            if (parentID == null)
            {
                query = from n in query
                        where n.ParentID == null
                        select n;
            }
            else
            {
                query = from n in query
                        where n.ParentID == parentID
                        select n;
            }

            query = from n in query
                    orderby n.OrderIndex, n.DateCreated
                    select n;

            var entities = query.ToList();

            var converter = new SurveyEntityModelConverter(DataContext);

            var models = (from n in entities
                          let m = converter.Convert(n)
                          select m).ToList();

            var model = new SurveysModel();
            model.List = models;

            surveysControl.Model = model;
        }
    }
}