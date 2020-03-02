using System;
using System.Web.Optimization;

namespace InBed.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //重新处理bundle忽略资源的规则
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region JS
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/BaseScripts").Include(
           "~/Template/Admin/InBed/js/jquery-1.10.2.min.js",
           "~/Template/Admin/InBed/js/jquery-migrate.js",
           "~/Template/Admin/InBed/js/bootstrap.min.js",
           "~/Template/Admin/InBed/js/modernizr.min.js",
           "~/Template/Admin/InBed/js/jquery.nicescroll.js",
           "~/Template/Admin/InBed/js/slidebars.min.js",
           "~/Template/Admin/InBed/js/switchery/switchery.min.js",
           "~/Template/Admin/InBed/js/switchery/switchery-init.js",
           "~/Template/Admin/InBed/js/sparkline/jquery.sparkline.js",
           "~/Template/Admin/InBed/js/sparkline/sparkline-init.js",
           "~/Template/Admin/InBed/js/jquery.validate.min.js",
           "~/Template/Admin/InBed/js/json2.js",
           "~/Template/Admin/InBed/js/scripts.js"));

            bundles.Add(new InBedScriptBundle("~/Template/Admin/InBed/JS/Layer/BaseLayer").Include(
                "~/Template/Admin/InBed/js/layer/layer.js"));

            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/InBed").Include(
                "~/Template/Admin/InBed/js/InBed.js"));

            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/InBedMenu").Include(
                "~/Template/Admin/InBed/js/InBed.menu.js"));



            //DataTable
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/DataTable").Include(
                "~/Template/Admin/InBed/js/data-table/js/jquery.dataTables.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.tableTools.min.js",
                "~/Template/Admin/InBed/js/data-table/js/bootstrap-dataTable.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.colVis.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.responsive.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.scroller.min.js",
                "~/Template/Admin/InBed/js/InBed.tables.js"));

            //DataTable Demo Page
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/DataTableDemo").Include(
                "~/Template/Admin/InBed/js/data-table/js/jquery.dataTables.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.tableTools.min.js",
                "~/Template/Admin/InBed/js/data-table/js/bootstrap-dataTable.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.colVis.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.responsive.min.js",
                "~/Template/Admin/InBed/js/data-table/js/dataTables.scroller.min.js",
                "~/Template/Admin/InBed/js/data-table-init.js"));

            //tree
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Tree").Include(
                "~/Template/Admin/InBed/js/fuelux/js/tree.min.js"));

            //login page
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Login").Include(
                "~/Template/Admin/InBed/js/jquery-1.10.2.min.js",
                "~/Template/Admin/InBed/js/bootstrap.min.js",
                "~/Template/Admin/InBed/js/jquery.validate.min.js",
                "~/Template/Admin/InBed/js/InBed.login.valid.js"));

            //select2 js
            bundles.Add(new ScriptBundle("~/JS/Admin/InBed/Select").Include(
                "~/Template/Admin/InBed/js/select2.js",
                "~/Template/Admin/InBed/js/select2-init.js"));

            //nesttable js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Nestable").Include(
                "~/Template/Admin/InBed/js/nestable/jquery.nestable.js"));

            //tagsinput js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Tags").Include(
                "~/Template/Admin/InBed/js/tags-input.js",
                "~/Template/Admin/InBed/js/tags-input-init.js"));

            //fileinput js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/File").Include(
                "~/Template/Admin/InBed/js/bootstrap-fileinput-master/js/fileinput.js",
                "~/Template/Admin/InBed/js/file-input-init.js"));

            //email js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Email").Include(
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/wysihtml5-0.3.0.js",
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/bootstrap-wysihtml5.js",
                "~/Template/Admin/InBed/js/bootstrap-fileinput-master/js/fileinput.js",
                "~/Template/Admin/InBed/js/file-input-init.js"));

            //editor js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/Editor").Include(
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/wysihtml5-0.3.0.js",
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/bootstrap-wysihtml5.js",
                "~/Template/Admin/InBed/js/summernote/dist/summernote.min.js"));

            //icheck js
            bundles.Add(new InBedScriptBundle("~/JS/Admin/InBed/FormValidate").Include(
                "~/Template/Admin/InBed/js/jquery.validate.min.js",
                "~/Template/Admin/InBed/js/form-validation-init.js",
                "~/Template/Admin/InBed/js/icheck/skins/icheck.min.js"));

            //Advance demo js
            bundles.Add(new ScriptBundle("~/JS/Admin/InBed/Advance").Include(
                "~/Template/Admin/InBed/js/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Template/Admin/InBed/js/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                "~/Template/Admin/InBed/js/bootstrap-daterangepicker/moment.min.js",
                "~/Template/Admin/InBed/js/bootstrap-daterangepicker/daterangepicker.js",
                "~/Template/Admin/InBed/js/bootstrap-colorpicker/js/bootstrap-colorpicker.js",
                "~/Template/Admin/InBed/js/bootstrap-timepicker/js/bootstrap-timepicker.js",
                "~/Template/Admin/InBed/js/picker-init.js",
                "~/Template/Admin/InBed/js/icheck/skins/icheck.min.js",
                "~/Template/Admin/InBed/js/icheck-init.js",
                "~/Template/Admin/InBed/js/tags-input.js",
                "~/Template/Admin/InBed/js/tags-input-init.js",
                "~/Template/Admin/InBed/js/touchspin.js",
                "~/Template/Admin/InBed/js/spinner-init.js",
                "~/Template/Admin/InBed/js/dropzone.js",
                "~/Template/Admin/InBed/js/select2.js",
                "~/Template/Admin/InBed/js/select2-init.js"));

            //flot chart demo
            bundles.Add(new ScriptBundle("~/JS/Admin/InBed/Chart").Include(
                "~/Template/admin/InBed/js/sparkline/jquery.sparkline.js",
                "~/Template/Admin/InBed/js/sparkline/sparkline-init.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.resize.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.tooltip.min.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.pie.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.selection.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.selection.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.stack.js",
                "~/Template/Admin/InBed/js/flot-chart/jquery.flot.crosshair.js",
                "~/Template/Admin/InBed/js/flot-chart-init.js"));

            //morris chart demo
            bundles.Add(new ScriptBundle("~/JS/Admin/InBed/ChartMorris").Include(
                "~/Template/admin/InBed/js/morris-chart/morris.js",
                "~/Template/Admin/InBed/js/morris-chart/raphael-min.js",
                "~/Template/Admin/InBed/js/morris-init.js"));

            //chartjs demo
            bundles.Add(new ScriptBundle("~/JS/Admin/InBed/ChartJs").Include(
                "~/Template/admin/InBed/js/chart-js/chart.js",
                "~/Template/Admin/InBed/js/chartJs-init.js"));

            bundles.Add(new InBedScriptBundle("~/JS/Front/Web/Home").Include(
                "~/Template/front/web/js/jquery.js",
                "~/Template/front/web/js/bootstrap.min.js",
                "~/Template/front/web/js/jquery.prettyPhoto.js",
                "~/Template/front/web/js/jquery.isotope.min.js",
                "~/Template/front/web/js/main.js",
                "~/Template/front/web/js/wow.min.js"));

            #endregion

            #region CSS
            //Base Styles
            bundles.Add(new StyleBundle("~/Assets/Css/BaseStyles").Include(
                       "~/assets/css/style.css",
                       "~/assets/css/responsive.css",
                        "~/assets/plugins/jquery-ui/smoothness/jquery-ui.min.css",
                         "~/assets/plugins/pace/pace-theme-flash.css",
                          "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                           "~/assets/plugins/bootstrap/css/bootstrap-theme.min.css",
                            "~/assets/fonts/font-awesome/css/font-awesome.css",
                             "~/assets/css/animate.min.css",
                               "~/assets/plugins/perfect-scrollbar/perfect-scrollbar.css"
                       ));

            //Base Styles
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/BaseStyles").Include(
                "~/Template/Admin/InBed/css/style.css",
                "~/Template/Admin/InBed/css/style-responsive.css"));

            //Data Table
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/DataTable").Include(
                "~/Template/Admin/InBed/js/data-table/css/jquery.dataTables.css",
                "~/Template/Admin/InBed/js/data-table/css/dataTables.tableTools.css",
                "~/Template/Admin/InBed/js/data-table/css/dataTables.colVis.min.css",
                "~/Template/Admin/InBed/js/data-table/css/dataTables.responsive.css",
                "~/Template/Admin/InBed/js/data-table/css/dataTables.scroller.css"));

            //tree
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/TreeStyle").Include(
                "~/Template/Admin/InBed/js/fuelux/css/tree-style.css"));

            //select2
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/SelectStyle").Include(
                "~/Template/Admin/InBed/css/select2.css",
                "~/Template/Admin/InBed/css/select2-bootstrap.css"));

            //Nestable
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/Nestable").Include(
                "~/Template/Admin/InBed/js/nestable/jquery.nestable.css"));
            //Tagsinput
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/Tags").Include(
                "~/Template/Admin/InBed/css/tagsinput.css"));

            //FileInput
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/File").Include(
                "~/Template/Admin/InBed/js/bootstrap-fileinput-master/css/fileinput.css"));

            //Email
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/Email").Include(
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/bootstrap-wysihtml5.css",
                "~/Template/Admin/InBed/js/bootstrap-fileinput-master/css/fileinput.css"));

            //Editor
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/Editor").Include(
                "~/Template/Admin/InBed/js/summernote/dist/summernote.css",
                "~/Template/Admin/InBed/js/bootstrap-wysihtml5/bootstrap-wysihtml5.css"));

            //icheck Demo
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/FormValidate").Include(
                "~/Template/admin/InBed/js/icheck/skins/all.css"));

            //morris chart Demo
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/ChartMorris").Include(
                "~/Template/admin/InBed/js/morris-chart/morris.css"));

            //Advance Demo
            bundles.Add(new StyleBundle("~/Template/Admin/InBed/Css/Advance").Include(
                "~/Template/admin/InBed/js/icheck/skins/all.css",
                "~/Template/admin/InBed/css/tagsinput.css",
                //"~/Template/admin/InBed/css/dropzone.css",
                "~/Template/admin/InBed/css/select2.css",
                "~/Template/admin/InBed/css/select2-bootstrap.css",
                "~/Template/admin/InBed/css/bootstrap-touchspin.css",
                "~/Template/admin/InBed/js/bootstrap-datepicker/css/datepicker.css",
                "~/Template/admin/InBed/js/bootstrap-timepicker/compiled/timepicker.css",
                "~/Template/admin/InBed/js/bootstrap-colorpicker/css/colorpicker.css",
                "~/Template/admin/InBed/js/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Template/admin/InBed/js/bootstrap-datetimepicker/css/datetimepicker.css"));



            //front web/index
            bundles.Add(new StyleBundle("~/css/front/web/home").Include(
                "~/Template/front/web/css/bootstrap.min.css",
                "~/Template/front/web/css/font-awesome.min.css",
                "~/Template/front/web/css/animate.min.css",
                "~/Template/front/web/css/prettyPhoto.css",
                "~/Template/front/web/css/main.css",
                "~/Template/front/web/css/responsive.css"));

            #endregion

            //BundleTable.EnableOptimizations = true;强制启用压缩
        }
        /// <summary>
        /// 添加bundle需要忽略的表达式的资源
        /// </summary>
        /// <param name="ignoreList"></param>
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException(nameof(ignoreList));

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}