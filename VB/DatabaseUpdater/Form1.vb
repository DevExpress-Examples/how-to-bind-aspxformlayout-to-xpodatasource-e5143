Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports PersistentObjects
Imports System.Globalization

Namespace DatabaseUpdater
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim connString As String = textBox1.Text
			XpoDefault.DataLayer = XpoDefault.GetDataLayer(connString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema)
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			XpoDefault.Session.UpdateSchema(GetType(PersistentObjects.Order).Assembly)
			XpoDefault.Session.CreateObjectTypeRecords()
			MessageBox.Show("Done!")
		End Sub

		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			Using uow As New UnitOfWork()
				If uow.FindObject(Of Customer)(Nothing) Is Nothing Then
					Dim cust As New Customer(uow)
					cust.CompanyName = "Alfreds Futterkiste"
					cust.ContactName = "Maria Anders"
					cust.Country = "Germany"
					cust.Phone = "030-0074321"


					Dim order As New Order(uow)
					order.OrderDate = Date.Parse("7/4/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 34.34D
					cust.Orders.Add(order)

					order = New Order(uow)
					order.OrderDate = Date.Parse("7/10/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 11.64D
					cust.Orders.Add(order)
					cust.Save()

					cust = New Customer(uow)
					cust.CompanyName = "Ana Trujillo Emparedados y helados"
					cust.ContactName = "Ana Trujillo"
					cust.Country = "Mexico"
					cust.Phone = "(5) 555-4729"
					cust.Save()

					order = New Order(uow)
					order.OrderDate = Date.Parse("7/12/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 65.81D
					cust.Orders.Add(order)

					order = New Order(uow)
					order.OrderDate = Date.Parse("7/15/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 41.34D
					cust.Orders.Add(order)

					order = New Order(uow)
					order.OrderDate = Date.Parse("7/11/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 51.50D
					cust.Orders.Add(order)
					cust.Save()

					cust = New Customer(uow)
					cust.CompanyName = "Antonio Moreno Taquería"
					cust.ContactName = "Antonio Moreno"
					cust.Country = "Mexico"
					cust.Phone = "(5) 555-4729"
					cust.Save()

					order = New Order(uow)
					order.OrderDate = Date.Parse("7/16/1996", CultureInfo.InvariantCulture)
					order.PaidTotal = 58.17D
					cust.Orders.Add(order)
					cust.Save()
				End If
				uow.CommitChanges()
			End Using
			MessageBox.Show("Done!")
		End Sub
	End Class
End Namespace