﻿using System.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class ReportEntityUnitModelConverter : SingleModelConverterBase<LP_Report, ReportUnitModel>
	{
		private readonly ReportLogicEntityModelConverter reportLogicConverter;

		public ReportEntityUnitModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
			reportLogicConverter = new ReportLogicEntityModelConverter(dbContext);
		}

		public override ReportUnitModel Convert(LP_Report source)
		{
			var target = new ReportUnitModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(ReportUnitModel target, LP_Report source)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Type = source.Type;
		    target.XLabelAngle = source.XLabelAngle;
			target.CategoryID = source.CategoryID;
			target.Description = source.Description;
			target.Interpretation = source.Interpretation;
			target.InformationSource = source.InformationSource;

			var logics = (from n in source.ReportLogics
						  where n.DateDeleted == null &&
								n.Logic != null
						  select n);

			var models = logics.Select(n => reportLogicConverter.Convert(n));

			target.ReportLogics = new ReportLogicsModel { List = models.ToList() };
		}
	}
}