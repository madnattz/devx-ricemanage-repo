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
       
        predefinedReportsUpdater.AddPredefinedReport(Of KP2Report)("ขพ.2", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP5Report)("ขพ.5", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP3Report)("สรุปแหล่งและแผนปฎิบัติการผลิตเมล็ดพันธุ์ ขพ.3 (สมข 20102)(ส่งส่วนกลาง)", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of KP3ReportForLocal)("สรุปแหล่งและแผนปฎิบัติการผลิตเมล็ดพันธุ์ ขพ.3 (สมข 20102)(ศูนย์)", GetType(MainPlan), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportWeight)("ใบชั่งน้ำหนัก", GetType(BuySeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of FarmerExpandSeason)("ทะเบียนเกษตรกรผู้จัดทำแปลงขยายพันธุ์ประจำฤดู", GetType(MainPlan), True)

        predefinedReportsUpdater.AddPredefinedReport(Of ReportK2)("1. ทะเบียนเกษตรกร (ก.2)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20101)("2.ความต้องการใช้เมล็ดพันธุ์เพื่อจัดทำแปลงขยายพันธุ์ (สมข 20101)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20103)("3. แผนและผลการจัดทำแปลงขยายพันธุ์ (สมข 20103)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20104)("4. ข้อมูลเกี่ยวกับการจัดทำแปลงขายพันธุ์ (สมข 20104)", GetType(MainPlan), False)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportSMK20105)("5. สรุปการใช้เมล็ดพันธุ์สำหรับจัดทำแปลงขยายพันธุ์ (สมข 20105)", GetType(MainPlan), False)

        '------------- Audit -----------------
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmReport)("1. แบบบันทึกการตรวจตัดสินคุณภาพแปลงขยายพันธุ์ข้าวอย่างเป็นทางการ", GetType(CheckFarm), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmSummaryReport)("2. รายงานผลการตรวจตัดสินคุณภาพแปลงขยายพันธุ์", GetType(CheckFarm), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityReport)("3. รายงานผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อยืนยันคุณภาพ/ก่อนการจัดทำแปลง", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QualityForBuyReport)("4. รายงานผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อจัดซื้อ", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of RedRiceReport)("5. รายงานปริมาณข้าวแดงที่ตรวจพบในการทดสอบคุณภาพเมล็ดพันธ์ข้าว", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityForMonth)("6. รายงานปริมาณการตรวจสอบคุณภาพเมล็ดพันธุ์ข้าวประจำเดือน", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of HistoryQualityReport)("7. ประวัติคุณภาพเมล็ดพันธุ์", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QAActivityPlanReport)("8. สรุปแผนและผลการดำเนินงานตามกิจกรรมในกระบวนการผลิตเมล็ดพันธุ์", GetType(QualityAudit), False)
        'predefinedReportsUpdater.AddPredefinedReport(Of QualityBetweenStorage)("9. รายงานผลการตรวจสอบคุณภาพเมล็ดพันธุ์ระหว่างการเก็บรักษา", GetType(QualityAudit), False)

        predefinedReportsUpdater.AddPredefinedReport(Of QAActivityPlanReport)("1. สรุปแผนและผลการดำเนินงานตามกิจกรรมในกระบวนการผลิตเมล็ดพันธุ์ (กคภ 2556/01)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityReport)("2. รายงานผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อยืนยันคุณภาพ (กคภ2556/02)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmReport)("3. รายงานผลการตรวจตัดสินคุณภาพแปลงขยายพันธุ์ (กคภ 2556/04)", GetType(CheckFarm), False)
        predefinedReportsUpdater.AddPredefinedReport(Of HistoryQualityReport)("4. ประวัติคุณภาพเมล็ดพันธุ์ (กคภ 2556/05)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of QualityBetweenStorage)("5. รายงานผลการตรวจสอบคุณภาพเมล็ดพันธุ์ระหว่างการเก็บรักษา ประจำเดือน (กคภ 2556/06)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of RedRiceReport)("6. รายงานปริมาณข้าวแดงที่ตรวจพบในการทดสอบคุณภาพเมล็ดพันธ์ข้าว (กคภ 2556/08)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckQualityForMonth)("7. รายงานปริมาณการตรวจสอบคุณภาพเมล็ดพันธุ์ข้าว ประจำเดือน (กคภ 2556/09)", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of QualityForBuyReport)("8. รายงานผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อจัดซื้อ", GetType(QualityAudit), False)
        predefinedReportsUpdater.AddPredefinedReport(Of CheckFarmSummaryReport)("9. รายงานผลการตรวจตัดสินคุณภาพแปลงขยายพันธุ์", GetType(CheckFarm), False)

        'Factory
        predefinedReportsUpdater.AddPredefinedReport(Of DailyProcess)("แบบสรุปผลการปรับปรุงสภาพเมล็ดพันธุ์รายวัน (สมข 20202)", GetType(FactoryInventory), False)
        predefinedReportsUpdater.AddPredefinedReport(Of PickProcess)("ใบนำส่งเมล็ดพันธุ์", GetType(FactoryReturnSeed), True)

        '------------- Sims ----------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of PickSeedReport)("ใบเบิก-จ่ายเมล็ดพันธุ์", GetType(PickSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TransferSeedReport)("ใบโอนเมล็ดพันธุ์", GetType(TransferSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of InvoiceSeedReport)("ใบส่งของและกำกับสินค้า", GetType(TransferSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReceiveSeedReport)("ใบรับเมล็ดพันธุ์เข้าโรงเก็บ", GetType(ReceiveSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of PickSeedReport)("ใบเบิก-จ่ายเมล็ดพันธุ์", GetType(PickSeed), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReceiveMaterialReport)("ใบรับวัสดุการผลิตเข้าโรงเก็บ", GetType(ReceiveMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of PickMaterialReport)("ใบเบิก-จ่าย วัสดุการผลิต", GetType(PickMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of RestoreMaterialReport)("ใบรับคืนวัสดุการผลิต", GetType(RestoreMaterial), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SaleDOI)("ใบส่งของและกำกับสินค้า", GetType(Sale), True)

        'predefinedReportsUpdater.AddPredefinedReport(Of AssociationCenter)("รายชื่อสมาชิกสมาคม/ชมรม", GetType(AssociationGroup), True)

        predefinedReportsUpdater.AddPredefinedReport(Of AssociationLocalReport)("รายชื่อสมาชิกสมาคม/ชมรม", GetType(AssociationGroup), True)
       
        '-------------------------------------- Account Report --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of GLReport)("01. บัญชีแยกประเภททั่วไป", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TrialBalance)("02. งบทดลอง02", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of TrialBalanceV2)("03. งบทดลอง02 (บัญชีหลัก)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of GLMonthly)("04. งบทดลองรายเดือน", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of GLMonthlyV2)("05. งบทดลองรายเดือน (บัญชีหลัก)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of StatementSheet)("06. งบดุล", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of StatementSheetV2)("07. งบดุล (บัญชีหลัก)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BalanceSheet)("08. งบกำไรขาดทุน", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BalanceSheetV2)("09. งบกำไรขาดทุน (บัญชีหลัก)", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of Cumulative)("10. รายจ่ายสะสม", GetType(AccountReportNav), True)
        '--------------------------------------------------------------------------------------------------------------

        predefinedReportsUpdater.AddPredefinedReport(Of ApproveBuyReport)("ขออนุมัติจัดซื้อ", GetType(ApproveBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyReport)("ขออนุมัติเบิกเงินค่าเมล็ดพันธุ์ซื้อคืน", GetType(SummaryBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyTransactionReport)("แบบสรุปการจัดซื้อเมล็ดพันธุ์รายวัน", GetType(SummaryBuy), True)


        predefinedReportsUpdater.AddPredefinedReport(Of AccountPayDate)("3. รายงานการเบิกจ่ายเงินค้าเมล็ดพันธุ์", GetType(AccountReportNav), True)
        'predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget01)("5. งบประมาณสงป01", GetType(AccountReportNav), True)
        'predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget02)("6. งบประมาณสงป02", GetType(AccountReportNav), True)

        '-------------------------------------- Account BookReport --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of CoverDaily)("1. ใบปะหน้ารายวันรับ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookDaily)("2. สมุดรายวันรับ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of CoverDailyExpenses)("3. ใบปะหน้ารายวันจ่าย", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookDailyExpenses)("4. สมุดรายวันจ่าย", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of CoverLeafThroughDaily)("5. ใบปะหน้าใบผ่านรายวัน", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of BookLeafThroughDaily)("6. ใบผ่านรายวัน", GetType(AccountReportNav), True)

        '-------------------------------------- Account OtherReport --------------------------------------------------------
        predefinedReportsUpdater.AddPredefinedReport(Of AccountBalance)("01. รายการยอดเงินคงเหลือ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountCost)("02. รายการต้นทุนการผลิต", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountPayDate)("03. รายงานการเบิกจ่ายเงินค้าเมล็ดพันธุ์", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportInvestmentDetail)("04. รายงาน รายรับ-รายจ่าย เงินทุนหมุนฯ ตาม GFMIS", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget01)("05. งบประมาณสงป301", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanBudget02)("06. งบประมาณสงป302", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountBringforward)("07. รายงานยอดยกมา", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of AccountForecast)("08. รายงานพยากรณ์รายได้", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportPlanTrialBalance03)("09. รายงาน แผนการรับ - จ่าย เงินประจำปีงบประมาณ", GetType(AccountReportNav), True)
        predefinedReportsUpdater.AddPredefinedReport(Of ReportIncomeAndExpensesOfMonth)("10. รายงาน ผลการรับ - จ่าย เงินประจำเดือน", GetType(AccountReportNav), True)

        predefinedReportsUpdater.AddPredefinedReport(Of ApproveBuyReport)("ขออนุมัติจัดซื้อ", GetType(ApproveBuy), True)
        predefinedReportsUpdater.AddPredefinedReport(Of SummaryBuyReport)("ขออนุมัติเบิกเงินค่าเมล็ดพันธุ์ซื้อคืน", GetType(SummaryBuy), True)
        Return New ModuleUpdater() {updater, predefinedReportsUpdater}
        '//Return New ModuleUpdater() {updater}
    End Function

    Public Overrides Sub Setup(application As XafApplication)
        MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
    End Sub
End Class
