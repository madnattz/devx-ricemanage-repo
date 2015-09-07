Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.Base

Public Class PublicEnum

    Public Enum PublicApprove
        <XafDisplayName("จัดทำข้อมูล"), ImageName("State_Validation_Skipped")>
        NotApprove = 0
        <XafDisplayName("อนุมัติ"), ImageName("State_Validation_Valid")>
        Approve = 1
    End Enum

    Public Enum PublicStatus
        <XafDisplayName("เปิดใช้งาน"), ImageName("Action_Workflow_Activate")>
        Active = 0
        <XafDisplayName("ปิดใช้งาน"), ImageName("Action_Workflow_DeActivate")>
        DeActive = 1
    End Enum

    Public Enum PlanFarmerStatus
        <XafDisplayName("อยู่ระหว่างดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("เสร็จสิ้นงานแปลง"), ImageName("State_Validation_Valid")>
        Approve = 1
    End Enum

    Public Enum PlanStatus
        <XafDisplayName("ระหว่างดำเนินการ"), ImageName("State_Validation_Valid")>
        Pending = 0
        <XafDisplayName("เสร็จสิ้นกระบวนการ"), ImageName("State_Task_Completed")>
        Done = 1
    End Enum

    Public Enum BuySeedType
        <XafDisplayName("เมล็ดควมชื้นสูง")>
        Wet = 0
        <XafDisplayName("เมล็ดแห้ง")>
        Dry = 1
        <XafDisplayName("เมล็ดควมชื้นสูงและเมล็ดแห้ง")>
        Both = 2
    End Enum

    Public Enum SiteType
        <XafDisplayName("ส่วนกลาง")> _
        Center = 1
        <XafDisplayName("ศูนย์วิจัยข้าว")> _
        Research = 2
        <XafDisplayName("ศูนย์เมล็ดพันธุ์ข้าว")> _
        Factory = 3
    End Enum

    Public Enum CheckResults 'ผลการการตรวจตัดสินแปลง
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("ผ่าน"), ImageName("State_Validation_Valid")>
        Pass = 1
        <XafDisplayName("ไม่ผ่าน"), ImageName("State_Validation_Invalid")>
        Fail = 2
        <XafDisplayName("เสียหายสิ้นเชิง"), ImageName("State_Validation_Invalid")>
        Fail2 = 3
    End Enum

    Public Enum AuditCheckResults 'ผลการการตรวจตัดสินแปลง
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("ผ่าน"), ImageName("State_Validation_Valid")>
        Pass = 1
        <XafDisplayName("ไม่ผ่าน"), ImageName("State_Validation_Invalid")>
        Fail = 2
    End Enum

    Public Enum ChooseResults 'ผลการการตรวจตัดสินแปลง
        <XafDisplayName("ผ่าน")>
        Pass = 1
        <XafDisplayName("ไม่ผ่าน")>
        Fail = 2
    End Enum

    Public Enum Disease 'โรค
        <XafDisplayName("ไม่มี")>
         None = 1
        <XafDisplayName("มีแต่ไม่มีผลเสียต่อคุณภาพเมล็ดพันธุ์")>
         NoEffective = 2
        <XafDisplayName("มีมากถึงระดับที่มีผลเสียหาย")>
         Effective = 3
    End Enum

    Public Enum Bug 'แมลง
        <XafDisplayName("ไม่มี")>
         None = 1
        <XafDisplayName("มีแต่ไม่มีผลเสียต่อคุณภาพเมล็ดพันธุ์")>
         NoEffective = 2
        <XafDisplayName("มีมากถึงระดับที่มีผลเสียหาย")>
         Effective = 3
    End Enum

    Public Enum Weed 'วัชพืช
        <XafDisplayName("ไม่มี")>
         None = 1
        <XafDisplayName("มีแต่ไม่มีผลเสียต่อคุณภาพเมล็ดพันธุ์")>
         NoEffective = 2
        <XafDisplayName("มีมากถึงระดับที่มีผลเสียหาย")>
         Effective = 3
    End Enum

    Public Enum Broken 'หักล้ม
        <XafDisplayName("ไม่มี")>
         None = 1
        <XafDisplayName("น้อยกว่า 1/3")>
         Less1_3 = 2
        <XafDisplayName("มากกว่า 1/3")>
         More1_3 = 3
    End Enum

    'Public Enum CheckPeriod
    '    <XafDisplayName("ออกดอก")> _
    '    FloweringPeriod = 1
    '    <XafDisplayName("โน้มรวง")> _
    '    BentPeriod = 2
    '    <XafDisplayName("เมล็ดสุกแก่")> _
    '    MaturatePeriod = 3

    'End Enum

    Public Enum CheckPeriod
        <XafDisplayName("กล้า")> _
        SeedingPeriod = 1
        <XafDisplayName("แตกกอ")> _
        TilleringPeriod = 2
        <XafDisplayName("ออกดอก")> _
        FloweringPeriod = 3
        <XafDisplayName("โน้มรวง")> _
        BentPeriod = 4
        <XafDisplayName("เมล็ดสุกแก่")> _
        MaturatePeriod = 5
    End Enum

    Public Enum PackageType
        <XafDisplayName("กระสอบปอ")> _
        NylonBag = 1
        <XafDisplayName("กระสอบพลาสติกสาน")> _
        PlasticBag = 2
    End Enum


    Public Enum BuyPositionType
        <XafDisplayName("คณะกรรมการจัดซื้อ")> _
        Buy = 0
        <XafDisplayName("คณะกรรมการตรวจรับ")> _
        Audit = 1
    End Enum

    Public Enum SimsStatus
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("อนุมัติ"), ImageName("State_Validation_Valid")>
        Approve = 1
        <XafDisplayName("ยกเลิก"), ImageName("Action_Deny")>
        Cancel = 2
        <XafDisplayName("เสร็จสิ้น"), ImageName("State_Task_Completed")>
        Closed = 3
    End Enum

    Public Enum SimsStatusSearch
        <XafDisplayName("")>
        None = -1
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("อนุมัติ"), ImageName("State_Validation_Valid")>
        Approve = 1
        <XafDisplayName("ยกเลิก"), ImageName("Action_Deny")>
        Cancel = 2

    End Enum

    Public Enum BuyStatus
        <XafDisplayName("ดำเนินการ"), ImageName("State_Validation_Skipped")>
        Pending = 0
        <XafDisplayName("อนุมัติ"), ImageName("State_Validation_Valid")>
        Approve = 1
        <XafDisplayName("ยกเลิก"), ImageName("State_Validation_Invalid")>
        Cancel = 2
    End Enum

    Public Enum SeedTransactionType
        <XafDisplayName("รับเมล็ดพันธุ์")> _
        Recieve = 0
        <XafDisplayName("ยกเลิกการรับเมล็ดพันธุ์")> _
        CancelRecieve = 1
        <XafDisplayName("จ่ายเมล็ดพันธุ์")> _
        Pick = 2
        <XafDisplayName("ยกเลิกการจ่ายเมล็ดพันธุ์")> _
        CancelPick = 3
        <XafDisplayName("จ่ายโอนเมล็ดพันธุ์")> _
        TransferOut = 4
        <XafDisplayName("ยกเลิกการจ่ายโอนเมล็ดพันธุ์")> _
        CancelTransferOut = 5
        <XafDisplayName("รับโอนเมล็ดพันธุ์")> _
        TransferIn = 6
        <XafDisplayName("ยกเลิกการรับโอนเมล็ดพันธุ์")> _
        CancelTransferIn = 7
        <XafDisplayName("จำหน่ายเมล็ดพันธุ์")> _
        Sale = 8
        <XafDisplayName("ยกเลิกการจำหน่ายเมล็ดพันธุ์")> _
        CancelSale = 9
        <XafDisplayName("จอง/สำรองเมล็ดพันธุ์")> _
        Reserve = 10
        <XafDisplayName("ยกเลิกการจอง/สำรองเมล็ดพันธุ์")> _
        CancelReserve = 11

    End Enum

    Public Enum MaterialTransactionType
        <XafDisplayName("รับวัสดุการผลิต")> _
        Recieve = 0
        <XafDisplayName("ยกเลิกการรับรับวัสดุการผลิต")> _
        CancelRecieve = 1
        <XafDisplayName("จ่ายวัสดุการผลิต")> _
        Pick = 2
        <XafDisplayName("ยกเลิกการจ่ายวัสดุการผลิต")> _
        CancelPick = 3
        <XafDisplayName("จ่ายโอนวัสดุการผลิต")> _
        TransferOut = 4
        <XafDisplayName("ยกเลิกการจ่ายโอนวัสดุการผลิต")> _
        CancelTransferOut = 5
        <XafDisplayName("รับโอนวัสดุการผลิต")> _
        TransferIn = 6
        <XafDisplayName("ยกเลิกการรับโอนวัสดุการผลิต")> _
        CancelTransferIn = 7
        <XafDisplayName("รับคืนวัสดุการผลิต")> _
        Restore = 8
        <XafDisplayName("ยกเลิกการรับคืนวัสดุการผลิต")> _
        CancelRestore = 9
    End Enum

    Public Enum FactoryTransactionType
        <XafDisplayName("จ่ายจากคลัง")> _
        Pick
        <XafDisplayName("ยกเลิกการจ่ายจากคลัง")> _
        CancelPick
        <XafDisplayName("ปรับปรุงสภาพเมล็ดพันธุ์")> _
        Process
        <XafDisplayName("ยกเลิกข้อมูลการปรับปรุงสภาพ")> _
        CancelProcess
    End Enum

    Public Enum SeedSource
        <XafDisplayName("แปลงขยายพันธุ์")> _
        Farmer = 1
        <XafDisplayName("แปลงสำรองพันธุ์")> _
        Center = 2
    End Enum

    Public Enum EnumFactoryNo
        <XafDisplayName("โรงงานที่ 1")>
        Factory1 = 1
        <XafDisplayName("โรงงานที่ 2")>
        Factory2 = 2
    End Enum

    Public Enum ReceiveForm
        <XafDisplayName("การปรับปรุงสภาพ")>
        ProcessSeed = 1
        <XafDisplayName("คณะกรรมการจัดซื้อ")>
        BuySeed = 2
        <XafDisplayName("การปรับปรุงรายการ")>
        AdjustAmount = 3
    End Enum

    Public Enum PickFor
        <XafDisplayName("การปรับปรุงสภาพ")>
        ProcessSeed = 1
        <XafDisplayName("คลุกสารเคมี")>
        ChemicalMix = 2
        <XafDisplayName("ตัดยอดเมล็ดพันธุ์เสื่อม")>
        AdjustBadSeed = 3
        <XafDisplayName("ตัดยอดเมล็ดพันธุ์คัดออก")>
        AdjustOutSeed = 4
        <XafDisplayName("ปรับปรุงรายการ")>
        AdjustAmount = 5
    End Enum

    Public Enum TakeFor
        <XafDisplayName("การปรับปรุงสภาพ")>
        ProcessSeed = 1
    End Enum

    Public Enum TransferType
        <XafDisplayName("จ่ายโอน")> _
        Send = 1
        <XafDisplayName("รับโอน")> _
        Recieve = 2

    End Enum

    Public Enum TransferFor
        <XafDisplayName("จำหน่าย")> _
        Sale = 1
        <XafDisplayName("จัดทำแปลง")> _
        Recieve = 2

    End Enum

    Public Enum MaterialType
        <XafDisplayName("วัสดุการผลิต")> _
        ProcessMaterail = 1
        <XafDisplayName("สารเคมีรมยา")> _
        FumigationChemical = 2
        <XafDisplayName("สารเคมีพ่นหมอก")> _
        SprayChemical = 3
        <XafDisplayName("น้ำมันอบลดความชื้น")> _
         DieselFuel = 4
    End Enum

    Public Enum BuyerType
        <XafDisplayName("ศูนย์ฯ")> _
        Local = 1
        <XafDisplayName("ส่วนกลาง")> _
        Center = 2
    End Enum

    Public Enum PickMaterialType
        <XafDisplayName("การปรับปรุงสภาพ")> _
        ProcessSeed = 1
        <XafDisplayName("ป้องกันแมลงศัตรูในโรงเก็บ")> _
        DefeatBug = 2
        <XafDisplayName("คลุกสารเคมี")> _
        ChemicalMix = 3
        <XafDisplayName("จำหน่ายเมล็ดพันธุ์")> _
        Sale = 4
        <XafDisplayName("บรรจุเมล็ดพันธุ์ซื้อคืน")> _
        Packaging = 5
    End Enum

    Public Enum FactoryProcessStatus
        <XafDisplayName("ระหว่างดำเนินการ"), ImageName("State_Validation_Skipped")> _
        Doing = 0
        <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ"), ImageName("State_Validation_Valid")> _
        Done = 1
    End Enum

    Public Enum CentralSaleType
        <XafDisplayName("จัดทำแปลง")> _
        Farming = 1
        <XafDisplayName("จำหน่าย")> _
        Sale = 2
    End Enum

    Public Enum ReserveBy
        <XafDisplayName("ส่วนกลาง")> _
        Center = 1
        <XafDisplayName("ศูนย์")> _
        Local = 2
    End Enum

    Public Enum ReserveType
        '<XafDisplayName("จอง")> _
        'Book = 1
        <XafDisplayName("สำรอง")> _
        Reserve = 1
    End Enum

    Public Enum MemberType
        <XafDisplayName("ส่วนราชการ")> _
        Official = 0
        <XafDisplayName("ตัวแทนจำหน่าย")> _
        Agent = 1
        <XafDisplayName("สมาชิกผู้ผลิตเมล็ดพันธุ์")> _
        MemberFarmer = 2
        <XafDisplayName("กลุ่มเกษตรกร")> _
        GroupFarmer = 3
        <XafDisplayName("เกษตรกรทั่วไป")> _
        Farmers = 4
    End Enum

    Public Enum MemberTypeSearch
        <XafDisplayName("")>
        None = -1
        <XafDisplayName("ส่วนราชการ")> _
        Official = 0
        <XafDisplayName("ตัวแทนจำหน่าย")> _
        Agent = 1
        <XafDisplayName("สมาชิกผู้ผลิตเมล็ดพันธุ์")> _
        MemberFarmer = 2
        <XafDisplayName("กลุ่มเกษตรกร")> _
        GroupFarmer = 3
        <XafDisplayName("เกษตรกรทั่วไป")> _
        Farmers = 4
    End Enum

    Public Enum CentralSaleSeedType
        <XafDisplayName("ประสบภัย")> _
        Danger = 1
        <XafDisplayName("โครงการ")> _
        Project = 2
    End Enum

    Public Enum PaidType
        <XafDisplayName("เงินสด")> _
        Cash = 1
        <XafDisplayName("เงินเชื่อ")> _
        Credit = 2
    End Enum

    Public Enum SaleType
        <XafDisplayName("ราคาเต็ม")> _
        NormalPrice = 1
        <XafDisplayName("ราคาส่วนลด")> _
        DiscountPrice = 2
        <XafDisplayName("ลดราคาเฉพาะกิจ")> _
        ExtraPrice = 3
        <XafDisplayName("ราคาทอดตลาด")> _
        AuctionPrice = 4
        <XafDisplayName("ราคาจำหน่ายบวกค่าขนส่ง")> _
        AddDeliveryPrice = 5
    End Enum

    Public Enum MainPlanLot
        <XafDisplayName("1")> _
        One = 1
        <XafDisplayName("2")> _
        Two = 2
        <XafDisplayName("3")> _
        Three = 3
        <XafDisplayName("เพิ่มเติม")> _
        Extra = 4
        <XafDisplayName("ชดเชย")> _
        Compensate = 5
    End Enum

    Public Enum GroupType
        <XafDisplayName("กลุ่มลงทะเบียน")> _
        RegisterGroup = 1
        <XafDisplayName("กลุ่มธรรมชาติ")> _
        UnRegisterGroup = 2
    End Enum

    Public Enum SeedGroup
        <XafDisplayName("ข้าวเจ้าไวต่อแสง")>
        Sensitive = 1
        <XafDisplayName("ข้าวไม่ไวต่อแสง")>
        NoneSensitive = 2
        <XafDisplayName("ข้าวหอมมะลิ")>
        Hommali = 3
        <XafDisplayName("ข้าวเหนียว")>
        Sticky = 4
        <XafDisplayName("เมล็ดพันธุ์อื่นๆ")>
        Other = 5
    End Enum

    Public Enum EnumAccountType
        <XafDisplayName("เดบิต")> _
        Debit = 0
        <XafDisplayName("เครดิต")> _
        Credit = 1
    End Enum

    Public Enum WeekNo
        <XafDisplayName("1-2")> _
        OneAndTwo = 1
        <XafDisplayName("3-4")> _
        ThreeAndFour
    End Enum

    Public Enum EnumMonth
        <XafDisplayName("--เลือกเดือน--")> _
        None = 0
        <XafDisplayName("มกราคม")> _
        JAN = 1
        <XafDisplayName("กุมภาพันธ์")> _
        FEB = 2
        <XafDisplayName("มีนาคม")> _
        MAR = 3
        <XafDisplayName("เมษายน")> _
        APR = 4
        <XafDisplayName("พฤษภาคม")> _
        MAY = 5
        <XafDisplayName("มิถุนายน")> _
        JUN = 6
        <XafDisplayName("กรกฎาคม")> _
        JUL = 7
        <XafDisplayName("สิงหาคม")> _
        AUG = 8
        <XafDisplayName("กันยายน")> _
        SEP = 9
        <XafDisplayName("ตุลาคม")> _
        OCT = 10
        <XafDisplayName("พฤศจิกายน")> _
        NOV = 11
        <XafDisplayName("ธันวาคม")> _
        DEC = 12
    End Enum

    Public Enum SubmitReportStatus
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")>
        Draft = 0
        <XafDisplayName("ส่งรายงานแล้ว"), ImageName("State_Validation_Valid")>
        Sent = 1
    End Enum

    Public Enum EnumBiweekly
        <XafDisplayName("--เลือกปักษ์--")> _
           None = 0
        <XafDisplayName("1")> _
            WeekOne = 1
        <XafDisplayName("2")> _
            WeekTwo = 2
    End Enum

    Public Enum ApproveBuyStatus
        <XafDisplayName("จัดทำข้อมูล"), ImageName("State_Validation_Skipped")>
        NotApprove = 0
        <XafDisplayName("อนุมัติ"), ImageName("State_Validation_Valid")>
        Approve = 1
        <XafDisplayName("เสร็จสิ้น"), ImageName("Action_Grant_Set")>
        Finish = 2
    End Enum

    Public Enum FactorySeedProcess
        <XafDisplayName("รอดำเนินการ"), ImageName("State_Validation_Skipped")> _
       Pending = 0
        <XafDisplayName("นำเข้าปรับปรุง"), ImageName("State_Validation_Valid")> _
        Process = 1
    End Enum

    Public Enum FactoryPickProcess
        <XafDisplayName("รอการส่งคืน"), ImageName("State_Validation_Skipped")> _
        Pending = 0
        <XafDisplayName("ส่งคืนเมล็ดพันธุ์"), ImageName("State_Validation_Valid")> _
        Process = 1
    End Enum

    Public Enum ActRequest
        <XafDisplayName("อยู่ระหว่างดำเนินการ"), ImageName("State_Task_Deferred")> _
        Request = 0
        <XafDisplayName("เข้าร่วม"), ImageName("State_Validation_Valid")> _
        Done = 1
        <XafDisplayName("ไม่เข้าร่วม"), ImageName("State_Validation_Invalid")> _
        Responce = 3
    End Enum

    Public Enum classifier
        <XafDisplayName("คน")>
        Human = 0
        <XafDisplayName("ครั้ง")>
        Unit = 1
        <XafDisplayName("บาท")>
        Bath = 2
    End Enum

    Public Enum EnumQuarter
        <XafDisplayName("ไตรมาส1")> _
        Quarter1 = 1
        <XafDisplayName("ไตรมาส2")> _
        Quarter2 = 2
        <XafDisplayName("ไตรมาส3")> _
        Quarter3 = 3
        <XafDisplayName("ไตรมาส4")> _
        Quarter4 = 4
    End Enum

End Class
