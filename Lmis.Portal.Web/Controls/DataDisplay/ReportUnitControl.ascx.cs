﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Models;
using System.Web.UI;
using CITI.EVO.Tools.Extensions;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Lmis.Portal.Web.Entites;
using System.Data;
using CITI.EVO.Tools.Utils;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
	public partial class ReportUnitControl : BaseExtendedControl<ReportUnitModel>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnCaptionsOK_OnClick(object sender, EventArgs e)
		{

		}

		protected void btnXYSeriesOK_OnClick(object sender, EventArgs e)
		{

		}

		protected override void OnSetModel(object model, Type type)
		{
			var unitModel = model as ReportUnitModel;
			if (unitModel == null)
				return;

			mainChart.DataSource = null;
			mainGrid.DataSource = null;

			pnlDescription.InnerHtml = unitModel.Description;

			var entities = GetQueries(unitModel);

			if (unitModel.Type == "Grid")
			{
				pnlChart.Visible = false;
				pnlGrid.Visible = true;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindGridData(entiry);
			}
			else
			{
				pnlChart.Visible = true;
				pnlGrid.Visible = false;

				var entiry = entities.FirstOrDefault();
				if (entiry != null)
					BindChartData(entiry);
			}
		}

		private void BindChartData(BindingInfoEntity entity)
		{
			lblChartTitle.Text = entity.Name;

			var chartType = GetChartType(entity.Type);
			var sqlDs = CreateDataSource(entity.SqlQuery);

			if (entity.Bindings == null)
				return;

			var modelLp = entity.Bindings.ToLookup(n => n.Caption);

			var modelsGrp = modelLp.FirstOrDefault();
			if (modelsGrp == null)
				return;

			var byTargetLp = modelsGrp.ToLookup(n => n.Target);

			var yMembers = byTargetLp["YValue"];
			var yMember = String.Join(",", yMembers.Select(n => n.Source));

			var xMembers = byTargetLp["XValue"];
			var xMember = String.Join(",", xMembers.Select(n => n.Source));

			var dataView = (DataView)sqlDs.Select(new DataSourceSelectArguments());
			if (dataView == null)
				return;

			FillCaptionsList(dataView, modelsGrp.Key);
			FillXYValuesList(dataView, xMember);

			var selCaptions = GetSelectedCaptions().ToHashSet();
			var selXYValues = GetSelectedXYSeries().ToHashSet();

			var collection = (from DataRowView n in dataView
							  select n);

			if (selCaptions.Count > 0)
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[modelsGrp.Key])
							  where selCaptions.Contains(v)
							  select n);
			}

			if (selXYValues.Count > 0)
			{
				collection = (from n in collection
							  let v = Convert.ToString(n[yMember])
							  where selXYValues.Contains(v)
							  select n);
			}

			mainChart.DataBindCrossTable(collection, modelsGrp.Key, xMember, yMember, String.Empty);

			foreach (var series in mainChart.Series)
			{
				series.Legend = "Default";
				series.BorderWidth = 2;
				series.ChartType = chartType;
				series.IsValueShownAsLabel = true;
				series.ToolTip = "#SERIESNAME #VALX/#VALY";
				series.Palette = ChartColorPalette.BrightPastel;
			}

			//var defaultTitle = mainChart.Titles["Default"];
			//if (defaultTitle != null)
			//	defaultTitle.Text = entity.Name;

			var leftTitle = mainChart.Titles["Left"];
			if (leftTitle != null)
				leftTitle.Text = xMember;

			var bottomTitle = mainChart.Titles["Bottom"];
			if (bottomTitle != null)
				bottomTitle.Text = yMember;

			mainChart.ApplyPaletteColors();
		}

		private void BindGridData(BindingInfoEntity entiry)
		{
			var sqlDs = CreateDataSource(entiry.SqlQuery);

			mainGrid.Columns.Clear();

			lblGridTitle.Text = entiry.Name;

			var keyFields = entiry.Bindings.Select(n => n.Source);
			var keyColumns = String.Join(";", keyFields);

			foreach (var model in entiry.Bindings)
			{
				var col = new GridViewDataColumn
				{
					Caption = model.Caption,
					FieldName = model.Source,
				};

				mainGrid.Columns.Add(col);
			}

			mainGrid.AutoGenerateColumns = (mainGrid.Columns.Count == 0);
			mainGrid.KeyFieldName = keyColumns;
			mainGrid.DataSource = sqlDs;
		}

		protected IEnumerable<BindingInfoEntity> GetQueries(ReportUnitModel unitModel)
		{
			var reportLogicsModel = unitModel.ReportLogics;
			if (reportLogicsModel == null || reportLogicsModel.List == null)
				yield break;

			foreach (var reportLogicModel in reportLogicsModel.List)
			{
				var logicModel = reportLogicModel.Logic;

				var list = (List<BindingInfoModel>)null;

				var bindings = reportLogicModel.Bindings;
				if (bindings == null || bindings.List == null)
					list = new List<BindingInfoModel>();
				else
					list = bindings.List;

				var entity = new BindingInfoEntity
				{
					Name = unitModel.Name,
					Type = reportLogicModel.Type,
					SqlQuery = logicModel.Query,
					Bindings = list,
				};

				if (logicModel.Type != "Query")
				{
					var queryGenerator = new QueryGenerator(DataContext, logicModel);
					entity.SqlQuery = queryGenerator.SelectQuery();
				}

				yield return entity;
			}
		}

		protected void FillCaptionsList(DataView dataView, String member)
		{
			FillFilterListBox(lstCaptions, dataView, member);
		}

		protected void FillXYValuesList(DataView dataView, String member)
		{
			FillFilterListBox(lstXYSeries, dataView, member);
		}

		protected void FillFilterListBox(CheckBoxList listBox, DataView dataView, String member)
		{
			var values = (from DataRowView n in dataView
						  let v = n[member]
						  orderby v
						  select v).Distinct().ToList();

			listBox.DataSource = values;
			listBox.DataBind();

			var selValues = GetListBoxSelectedValues(listBox).ToHashSet();

			foreach (ListItem listItem in listBox.Items)
				listItem.Selected = selValues.Contains(listItem.Value);
		}

		protected IEnumerable<String> GetSelectedCaptions()
		{
			return GetListBoxSelectedValues(lstCaptions);
		}

		protected IEnumerable<String> GetSelectedXYSeries()
		{
			return GetListBoxSelectedValues(lstXYSeries);
		}

		protected IEnumerable<String> GetListBoxSelectedValues(CheckBoxList listBox)
		{
			var values = (from n in Request.Form.AllKeys
						  where n.StartsWith(listBox.UniqueID)
						  let m = Request.Form[n]
						  select m);

			return values;
		}


		private SeriesChartType GetChartType(String type)
		{
			SeriesChartType value;
			if (Enum.TryParse(type, true, out value))
				return value;

			return SeriesChartType.Line;
		}

		private SqlDataSource CreateDataSource(String sqlQuery)
		{
			var sqlDs = new SqlDataSource
			{
				ConnectionString = GetConnectionString(),
				SelectCommand = sqlQuery,
				CacheKeyDependency = sqlQuery.ComputeMd5(),
				CacheExpirationPolicy = DataSourceCacheExpiry.Sliding,
				CacheDuration = 900,
				EnableCaching = true,
			};

			return sqlDs;
		}

		private String GetConnectionString()
		{
			var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
			return connString.ConnectionString;
		}
	}
}