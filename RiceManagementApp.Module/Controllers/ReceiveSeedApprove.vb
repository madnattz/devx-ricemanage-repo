Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Layout
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Model.NodeGenerators

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
Partial Public Class ReceiveSeedApprove
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        ' Perform various tasks depending on the target View.
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub

    Private Sub SetToApprove_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SetToApprove.Execute

        For Each item As ReceiveSeed In e.SelectedObjects

            If Not item.ReceiveSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            Dim ops As IObjectSpace = Application.CreateObjectSpace

            Try
                item.Status = PublicEnum.SimsStatus.Approve
                '//สร้าง Product
                '//MyBase.Save()
                If item.ReceiveSeedDetails.Count > 0 Then
                    Dim criteria As String = "SeedStatus=? and SeedType= ? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and MoneyType=?"
                    For i As Integer = 0 To item.ReceiveSeedDetails.Count - 1
                        'Dim _SeedType As SeedType = item.ReceiveSeedDetails(i).SeedType
                        'Dim _SeedStatus As SeedStatus = item.ReceiveSeedDetails(i).SeedStatus
                        'Dim _SeedClass As SeedClass = item.ReceiveSeedDetails(i).SeedClass
                        'Dim _Season As Season = item.ReceiveSeedDetails(i).Season
                        'Dim _SeedYear As String = item.ReceiveSeedDetails(i).SeedYear
                        'Dim _SeedLot As Integer = item.ReceiveSeedDetails(i).LotNo
                        'Dim _MoneyType As MoneyType = item.ReceiveSeedDetails(i).MoneyType

                        Dim objProduct As SeedProduct = ObjectSpace.FindObject(Of SeedProduct) _
                                                        (CriteriaOperator.Parse(criteria, item.ReceiveSeedDetails(i).SeedStatus,
                                                                                            item.ReceiveSeedDetails(i).SeedType, _
                                                                                            item.ReceiveSeedDetails(i).SeedClass, _
                                                                                             item.ReceiveSeedDetails(i).Season, _
                                                                                            item.ReceiveSeedDetails(i).SeedYear, _
                                                                                            item.ReceiveSeedDetails(i).LotNo, _
                                                                                            item.ReceiveSeedDetails(i).MoneyType))
                        If objProduct Is Nothing Then
                            Dim objNewProduct As SeedProduct = ObjectSpace.CreateObject(Of SeedProduct)()

                            Dim _PlantID As String = item.ReceiveSeedDetails(i).Plant.PlantID
                            Dim _SeedStatusID As String = item.ReceiveSeedDetails(i).SeedStatus.SeedStatusID.ToString
                            Dim _SeedTypeID As String = String.Format("{0:D2}", Convert.ToInt32(item.ReceiveSeedDetails(i).SeedType.SeedID))
                            Dim _ClassID As String = item.ReceiveSeedDetails(i).SeedClass.ClassID
                            Dim _SeasonID As String = item.ReceiveSeedDetails(i).Season.SeasonID
                            Dim _Year As String = item.ReceiveSeedDetails(i).SeedYear
                            Dim _MoneyTypeId As String = item.ReceiveSeedDetails(i).MoneyType.MoneyTypeId
                            Dim _LotNo As String = item.ReceiveSeedDetails(i).LotNo.ToString("D3")

                            '1-62-2-1-2556-1-001
                            objNewProduct.ProductCode = String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", _
                                                                      _PlantID, _
                                                                      _SeedStatusID, _
                                                                      _SeedTypeID, _
                                                                      _ClassID, _
                                                                      _SeasonID, _
                                                                      _Year, _
                                                                      _MoneyTypeId, _
                                                                      _LotNo)
                            objNewProduct.ProductName = item.ReceiveSeedDetails(i).Plant.PlantName & "-" & _
                                                       item.ReceiveSeedDetails(i).SeedStatus.SeedStatusName & "-" & _
                                                       item.ReceiveSeedDetails(i).SeedType.SeedName & "-" & _
                                                       item.ReceiveSeedDetails(i).SeedClass.ClassName & "-" & _
                                                       item.ReceiveSeedDetails(i).Season.SeasonName & "-" & _
                                                        item.ReceiveSeedDetails(i).SeedYear & "-" & _
                                                       item.ReceiveSeedDetails(i).MoneyType.MoneyTypeName & "-" & _
                                                       item.ReceiveSeedDetails(i).LotNo.ToString("D3")
                            objNewProduct.Plant = item.ReceiveSeedDetails(i).Plant
                            objNewProduct.SeedStatus = item.ReceiveSeedDetails(i).SeedStatus
                            objNewProduct.SeedType = item.ReceiveSeedDetails(i).SeedType
                            objNewProduct.SeedClass = item.ReceiveSeedDetails(i).SeedClass
                            objNewProduct.Season = item.ReceiveSeedDetails(i).Season
                            objNewProduct.SeedYear = item.ReceiveSeedDetails(i).SeedYear
                            objNewProduct.MoneyType = item.ReceiveSeedDetails(i).MoneyType
                            objNewProduct.LotNo = item.ReceiveSeedDetails(i).LotNo

                            '// จำนวนคงคลังครั้งแรก
                            objNewProduct.TotalStockAmount = item.ReceiveSeedDetails(i).Quantity
                            objNewProduct.AvailableAmount = item.ReceiveSeedDetails(i).Quantity
                            objNewProduct.CollectAmount = item.ReceiveSeedDetails(i).Quantity

                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            item.ReceiveSeedDetails(i).SeedProduct = objNewProduct

                            '// Insert Transaction Data
                            '//InsertTransaction(ReceiveSeedDetails(i), ActionType.Approve)
                            '//TransactionControl.InsertSeedTransaction(ops, PublicEnum.SeedTransactionType.Recieve, fRefNo, ReceiveSeedDetails(i).SeedProduct, ReceiveSeedDetails(i).Quantity, TransactionControl.StockType.Income)
                            Dim objTransactionLog As SeedTransaction = ObjectSpace.CreateObject(Of SeedTransaction)() 'ops, PublicEnum.SeedTransactionType.Recieve, item.ReceiveNo, item.ReceiveSeedDetails(i).SeedProduct, item.ReceiveSeedDetails(i).Quantity)

                            objNewProduct.Save()
                        Else
                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            item.ReceiveSeedDetails(i).SeedProduct = objProduct

                            '// Update ยอดคงคลัง(บวกเพิ่ม) 
                            objProduct.TotalStockAmount += item.ReceiveSeedDetails(i).Quantity
                            objProduct.AvailableAmount += item.ReceiveSeedDetails(i).Quantity
                            objProduct.CollectAmount += item.ReceiveSeedDetails(i).Quantity
                            '// Insert Transaction Data
                            '//InsertTransaction(ReceiveSeedDetails(i), ActionType.Approve)
                            Dim objTransactionLog As New SeedTransaction(ops, PublicEnum.SeedTransactionType.Recieve, item.RefNo, item.ReceiveSeedDetails(i).SeedProduct, item.ReceiveSeedDetails(i).Quantity)

                        End If

                        '//เพิ่มข้อมูลสำหรับการอ้างอิง Lot
                        If item.ReceiveSeedDetails(i).SeedStatus.SeedStatusName.Contains("ดี") Then
                            SaveToReferenceLot(ops, item.ReceiveSeedDetails(i).RefLot, item.ReceiveSeedDetails(i))
                        End If

                    Next
                    '//Session.CommitTransaction()
                End If

                item.Save()

                ops.CommitChanges()
                View.ObjectSpace.CommitChanges()

            Catch ex As Exception
                item.Status = PublicEnum.SimsStatus.Pending
                ops.Rollback()
            End Try
        Next
    End Sub

    Public Sub SaveToReferenceLot(os As IObjectSpace, objPick As PickSeedDetail, objReceive As ReceiveSeedDetail)
        Dim objRefLot As New ReferenceLotNo(os)
        objRefLot.Pick = objPick
        objRefLot.Receive = objReceive
        objRefLot.Save()
    End Sub

End Class
