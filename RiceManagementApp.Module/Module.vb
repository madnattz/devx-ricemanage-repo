Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.ExpressApp.ReportsV2

Public NotInheritable Class [RiceManagementAppModule]
    Inherits ModuleBase
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        '//Reports 
        Dim predefinedReportsUpdater As New PredefinedReportsUpdater(Application, objectSpace, versionFromDB)
       
        predefinedReportsUpdater.AddPredefinedReport(Of KP2Report)("��.2", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP5Report)("��.5", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP3Report)("��ػ�������Ἱ��Ժѵԡ�ü�Ե���紾ѹ��� ��.3 (��� 20102)(����ǹ��ҧ)", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP3ReportForLocal)("��ػ�������Ἱ��Ժѵԡ�ü�Ե���紾ѹ��� ��.3 (��� 20102)(�ٹ��)", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportWeight)("㺪�觹��˹ѡ", GetType(BuySeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of FarmerExpandSeason)("����¹�ɵáü��Ѵ���ŧ���¾ѹ����Ш�Ĵ�", GetType(MainPlan), True)

        predefinedReportsUpdater.AddPredefinedReport(Of ReportK2)("1. ����¹�ɵá� (�.2)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20101)("2.������ͧ��������紾ѹ������ͨѴ���ŧ���¾ѹ��� (��� 20101)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20103)("3. Ἱ��мš�èѴ���ŧ���¾ѹ��� (��� 20103)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20104)("4. ����������ǡѺ��èѴ���ŧ��¾ѹ��� (��� 20104)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20105)("5. ��ػ��������紾ѹ�������Ѻ�Ѵ���ŧ���¾ѹ��� (��� 20105)", GetType(MainPlan), False)

        '------------- Audit -----------------
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmReport)("1. Ẻ�ѹ�֡��õ�Ǩ�Ѵ�Թ�س�Ҿ�ŧ���¾ѹ���������ҧ�繷ҧ���", GetType(CheckFarm), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmSummaryReport)("2. ��§ҹ�š�õ�Ǩ�Ѵ�Թ�س�Ҿ�ŧ���¾ѹ���", GetType(CheckFarm), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityReport)("3. ��§ҹ�š�÷��ͺ�س�Ҿ���紾ѹ��������׹�ѹ�س�Ҿ/��͹��èѴ���ŧ", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QualityForBuyReport)("4. ��§ҹ�š�÷��ͺ�س�Ҿ���紾ѹ������ͨѴ����", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of RedRiceReport)("5. ��§ҹ����ҳ����ᴧ����Ǩ��㹡�÷��ͺ�س�Ҿ���紾ѹ�����", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityForMonth)("6. ��§ҹ����ҳ��õ�Ǩ�ͺ�س�Ҿ���紾ѹ�����ǻ�Ш���͹", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of HistoryQualityReport)("7. ����ѵԤس�Ҿ���紾ѹ���", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QAActivityPlanReport)("8. ��ػἹ��мš�ô��Թ�ҹ����Ԩ����㹡�кǹ��ü�Ե���紾ѹ���", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QualityBetweenStorage)("9. ��§ҹ�š�õ�Ǩ�ͺ�س�Ҿ���紾ѹ��������ҧ������ѡ��", GetType(QualityAudit), False)

        predefinedReportsUpdater.AddPredefinedReport(Of QAActivityPlanReport)("1. ��ػἹ��мš�ô��Թ�ҹ����Ԩ����㹡�кǹ��ü�Ե���紾ѹ��� (��� 2556/01)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityReport)("2. ��§ҹ�š�÷��ͺ�س�Ҿ���紾ѹ��������׹�ѹ�س�Ҿ (���2556/02)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmReport)("3. ��§ҹ�š�õ�Ǩ�Ѵ�Թ�س�Ҿ�ŧ���¾ѹ��� (��� 2556/04)", GetType(CheckFarm), False)
        predefinedReportsUpdater.AddPredefinedReport(Of HistoryQualityReport)("4. ����ѵԤس�Ҿ���紾ѹ��� (��� 2556/05)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of QualityBetweenStorage)("5. ��§ҹ�š�õ�Ǩ�ͺ�س�Ҿ���紾ѹ��������ҧ������ѡ�� ��Ш���͹ (��� 2556/06)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of RedRiceReport)("6. ��§ҹ����ҳ����ᴧ����Ǩ��㹡�÷��ͺ�س�Ҿ���紾ѹ����� (��� 2556/08)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityForMonth)("7. ��§ҹ����ҳ��õ�Ǩ�ͺ�س�Ҿ���紾ѹ������ ��Ш���͹ (��� 2556/09)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of QualityForBuyReport)("8. ��§ҹ�š�÷��ͺ�س�Ҿ���紾ѹ������ͨѴ����", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmSummaryReport)("9. ��§ҹ�š�õ�Ǩ�Ѵ�Թ�س�Ҿ�ŧ���¾ѹ���", GetType(CheckFarm), False)

        'Factory
        predefinedReportsUpdater.AddPredefinedReport(Of DailyProcess)("Ẻ��ػ�š�û�Ѻ��ا��Ҿ���紾ѹ�������ѹ (��� 20202)", GetType(FactoryInventory), False)
        predefinedReportsUpdater.AddPredefinedReport(Of PickProcess)("㺹������紾ѹ���", GetType(FactoryReturnSeed), True)

        '------------- Sims ----------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of PickSeedReport)("��ԡ-�������紾ѹ���", GetType(PickSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TransferSeedReport)("��͹���紾ѹ���", GetType(TransferSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of InvoiceSeedReport)("��觢ͧ��СӡѺ�Թ���", GetType(TransferSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReceiveSeedReport)("��Ѻ���紾ѹ�������ç��", GetType(ReceiveSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of PickSeedReport)("��ԡ-�������紾ѹ���", GetType(PickSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReceiveMaterialReport)("��Ѻ��ʴء�ü�Ե����ç��", GetType(ReceiveMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of PickMaterialReport)("��ԡ-���� ��ʴء�ü�Ե", GetType(PickMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of RestoreMaterialReport)("��Ѻ�׹��ʴء�ü�Ե", GetType(RestoreMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SaleDOI)("��觢ͧ��СӡѺ�Թ���", GetType(Sale), True)

        'predefinedReportsUpdater.AddPredefinedReport(Of AssociationCenter)("��ª�����Ҫԡ��Ҥ�/����", GetType(AssociationGroup), True)

        predefinedReportsUpdater.AddPredefinedReport(Of AssociationLocalReport)("��ª�����Ҫԡ��Ҥ�/����", GetType(AssociationGroup), True)
       
        '-------------------------------------- Account Report --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of GLReport)("01. �ѭ���¡�����������", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TrialBalance)("02. �����ͧ02", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TrialBalanceV2)("03. �����ͧ02 (�ѭ����ѡ)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of GLMonthly)("04. �����ͧ�����͹", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of GLMonthlyV2)("05. �����ͧ�����͹ (�ѭ����ѡ)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of StatementSheet)("06. �����", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of StatementSheetV2)("07. ����� (�ѭ����ѡ)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BalanceSheet)("08. �����âҴ�ع", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BalanceSheetV2)("09. �����âҴ�ع (�ѭ����ѡ)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of Cumulative)("10. ��¨�������", GetType(AccountReportNav), True)
        '--------------------------------------------------------------------------------------------------------------

        predefinedReportsUpdater.AddPredefinedReport(Of ApproveBuyReport)("��͹��ѵԨѴ����", GetType(ApproveBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyReport)("��͹��ѵ��ԡ�Թ������紾ѹ�����ͤ׹", GetType(SummaryBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyTransactionReport)("Ẻ��ػ��èѴ�������紾ѹ�������ѹ", GetType(SummaryBuy), True)


        predefinedReportsUpdater.AddPredefinedReport(Of AccountPayDate)("3. ��§ҹ����ԡ�����Թ������紾ѹ���", GetType(AccountReportNav), True)
        'predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget01)("5. ������ҳʧ�01", GetType(AccountReportNav), True)
        'predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget02)("6. ������ҳʧ�02", GetType(AccountReportNav), True)

        '-------------------------------------- Account BookReport --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of CoverDaily)("1. 㺻�˹������ѹ�Ѻ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookDaily)("2. ��ش����ѹ�Ѻ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of CoverDailyExpenses)("3. 㺻�˹������ѹ����", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookDailyExpenses)("4. ��ش����ѹ����", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of CoverLeafThroughDaily)("5. 㺻�˹��㺼�ҹ����ѹ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookLeafThroughDaily)("6. 㺼�ҹ����ѹ", GetType(AccountReportNav), True)

        '-------------------------------------- Account OtherReport --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of AccountBalance)("01. ��¡���ʹ�Թ�������", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountCost)("02. ��¡�õ鹷ع��ü�Ե", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountPayDate)("03. ��§ҹ����ԡ�����Թ������紾ѹ���", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportInvestmentDetail)("04. ��§ҹ ����Ѻ-��¨��� �Թ�ع��ع� ��� GFMIS", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget01)("05. ������ҳʧ�301", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget02)("06. ������ҳʧ�302", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountBringforward)("07. ��§ҹ�ʹ¡��", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountForecast)("08. ��§ҹ��ҡó������", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanTrialBalance03)("09. ��§ҹ Ἱ����Ѻ - ���� �Թ��Шӻէ�����ҳ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportIncomeAndExpensesOfMonth)("10. ��§ҹ �š���Ѻ - ���� �Թ��Ш���͹", GetType(AccountReportNav), True)

        predefinedReportsUpdater.AddPredefinedReport(Of ApproveBuyReport)("��͹��ѵԨѴ����", GetType(ApproveBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyReport)("��͹��ѵ��ԡ�Թ������紾ѹ�����ͤ׹", GetType(SummaryBuy), True)
        Return New ModuleUpdater() {updater, predefinedReportsUpdater}
        '//Return New ModuleUpdater() {updater}
    End Function

    Public Overrides Sub Setup(application As XafApplication)
        MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
    End Sub
End Class
