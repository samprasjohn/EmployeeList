<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="cuteQM19.Cutech.Express.Inspection.Masters.EmployeeList" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function SaveEmployee() {
            EmployeeGrid.UpdateEdit();
        }
        function CancelEmployee() {
            EmployeeGrid.UpdateEdit();
        }
        function customCallbackFunction(rowIdx) {
    EmployeeGrid.PerformCallback(rowIdx.toString());
}
    </script>
</head>
<body>
    <form runat="server">
 
        <dx:ASPxGridView ID="EmployeeGrid" runat="server" DataSourceID="EmployeeObjectDataSource"
            KeyFieldName="EmployeeID" Theme="Office2003Blue" ClientInstanceName="EmployeeGrid" Width="100%"
            AutoGenerateColumns="False" OnRowInserting="EmployeeGrid_RowInserting"
            OnRowUpdating="EmployeeGrid_RowUpdating" SettingsText-EmptyDataRow="<%$ Resources:Resource, NODATATODISPLAY%>">
            <ClientSideEvents EndCallback="function(s, e)
                                               {                                                  
                                                    if (s.cpIsUpdated != '' && s.cpIsUpdated !=  null) { BaseMessage(s.cpIsUpdated);s.cpIsUpdated=''; }
                                               }" />
            <SettingsDetail IsDetailGrid="false" ShowDetailButtons="False" />
            <SettingsBehavior ConfirmDelete="True" />
            <Settings ShowTitlePanel="true" />
            <SettingsEditing Mode="EditForm" />
            <SettingsCommandButton>
                <EditButton>
                    <Image Url="~/Inspection/images/icon/edit.png" ToolTip="Edit">
                    </Image>
                </EditButton>
                <CancelButton Text="Cancel" ButtonType="Button" />
                <DeleteButton>
                    <Image Url="~/Inspection/images/icon/Delete.png" ToolTip="Delete">
                    </Image>
                </DeleteButton>
                <CancelButton Text="Cancel" ButtonType="Button" />
                <UpdateButton Text="Save" ButtonType="Button" />
            </SettingsCommandButton>
            <Columns>
                <dx:GridViewCommandColumn Width="10px" HeaderStyle-HorizontalAlign="Center"
                    ButtonType="Image" Caption="Edit" VisibleIndex="0" AllowDragDrop="False"
                    ShowInCustomizationForm="True" ShowEditButton="True" ShowCancelButton="True">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Width="10px" HeaderStyle-HorizontalAlign="Center" ButtonType="Image"
                    Caption="Delete" VisibleIndex="0" AllowDragDrop="False" ShowInCustomizationForm="True" ShowDeleteButton="True" ShowCancelButton="True">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="Name"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Salary"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Experience"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Gender"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DesiredLocation"></dx:GridViewDataColumn>
            </Columns>

            <Templates>
                <EditForm>
                    <table>
                        <tr>
                            <td>                                
                                <dx:ASPxLabel ID="LabelName" runat="server" Text="Name"></dx:ASPxLabel>
                                <dx:ASPxTextBox ID="TextBoxName" runat="server" Text='<%#Bind("Name")%>'></dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="LabelSalary" runat="server" Text="Salary"></dx:ASPxLabel>
                                <dx:ASPxTextBox ID="TextBoxSalary" runat="server" Text='<%#Bind("Salary")%>'></dx:ASPxTextBox>
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="LabelExperience" runat="server" Text="Experience"></dx:ASPxLabel>
                                <dx:ASPxTextBox ID="TextBoxExperience" runat="server" Text='<%#Bind("Experience")%>'></dx:ASPxTextBox>
                            </td>
                            <td>
                                    <dx:ASPxLabel ID="LabelGender" runat="server" Text="Gender"></dx:ASPxLabel>
                                    <dx:ASPxRadioButton ID="RadioMale" runat="server" GroupName="Gender" Text="Male">
                                    </dx:ASPxRadioButton>
                <dx:ASPxRadioButton ID="RadioFemale" runat="server" GroupName="Gender" Text="Female">
                </dx:ASPxRadioButton>                              
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="LabelDesiredLocation" runat="server" Text="DesiredLocation"></dx:ASPxLabel>
                                <dx:ASPxCheckBox ID="CheckBoxIndia" runat="server" GroupName="DesiredLocation" Text="India">
                                </dx:ASPxCheckBox>
                                <dx:ASPxCheckBox ID="CheckBoxSingapore" runat="server" GroupName="DesiredLocation" Text="Singapore">
                                </dx:ASPxCheckBox>
                                <dx:ASPxCheckBox ID="CheckBoxChina" runat="server" GroupName="DesiredLocation" Text="China">
                                </dx:ASPxCheckBox>
</td>
                        </tr>
                        <tr>
            <td colspan="2" style="text-align:center;">
                <dx:ASPxButton ID="ButtonSave" runat="server" Text="Save" AutoPostBack="false"
                    Theme="Office2003Blue" OnClick="ButtonSave_Click">
                </dx:ASPxButton>
                <dx:ASPxButton ID="ButtonCancel" runat="server" Text="Cancel" AutoPostBack="false"
                    Theme="Office2003Blue">
                    <ClientSideEvents Click="function(s,e){ CancelEmployee(); }" />
                </dx:ASPxButton>
            </td>
        </tr>
                    </table>
                </EditForm>
                <TitlePanel>
                    <table>
                        <tr>
                            <td>
                                <div style="display: flex; align-items: center;">
                                    <span style="margin-right: 10px;">Employee List</span>
                                </div>
                            </td>
                            <td>
                                <dx:ASPxButton ID="ButtonAdd" runat="server" Text="Add" EnableTheming="true" AutoPostBack="false"
                                    Font-Size="Small" HoverStyle-Font-Italic="true" ForeColor="White" RenderMode="Link"
                                    OnClick="ButtonAdd_Click">                    
                                    <Image>
                                        <SpriteProperties CssClass="addImageBut" />
                                    </Image>
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ButtonAFIViewPDFExport" runat="server" Text="PDF" EnableTheming="true" AutoPostBack="false"
                                    Font-Size="Small" HoverStyle-Font-Italic="true" ForeColor="White" RenderMode="Link"
                                    ToolTip="<%$ Resources:Resource, EXPORTTOPDF%>" OnClick="ButtonExport_Click">
                                    <Image>
                                        <SpriteProperties CssClass="pdfImageBut" />
                                    </Image>
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ButtonAFIViewExcelExport" runat="server" Text="EXCEL" EnableTheming="true" AutoPostBack="false"
                                    Font-Size="Small" HoverStyle-Font-Italic="true" ForeColor="White" RenderMode="Link"
                                    ToolTip="<%$ Resources:Resource, EXPORTTOEXCEL %>" OnClick="ButtonExport_Click">
                                    <Image>
                                        <SpriteProperties CssClass="excelImageBut" />
                                    </Image>
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ButtonFilter" runat="server" EnableTheming="False" RenderMode="Link" AutoPostBack="false"
                                    ToolTip="<%$ Resources:Resource, SHOWHIDEFILTER%>" OnClick="ButtonFilter_Click"
                                    Font-Size="Small" HoverStyle-Font-Italic="true" Text="<%$ Resources:Resource, FILTER%>" ForeColor="White">
                                    <Image>
                                        <SpriteProperties CssClass="filterImageBut" />
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <Settings ShowTitlePanel="true" />
        </dx:ASPxGridView>

        <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="EmployeeGrid">
        </dx:ASPxGridViewExporter>
       
    </form>
    <asp:ObjectDataSource ID="EmployeeObjectDataSource" runat="server"
        SelectMethod="Ins_EmployeeSelect" InsertMethod="Ins_EmployeeInsert"
        UpdateMethod="Ins_EmployeeUpdate" DeleteMethod="Ins_EmployeeDelete"
        OnInserted="EmployeeObjectDataSource_Inserted"
        OnUpdated="EmployeeObjectDataSource_Updated"
        OnDeleted="EmployeeObjectDataSource_Deleted"
        TypeName="Cutech.Express.DataAccess.InspectionDataAccess">
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Salary" Type="Int32" />
            <asp:Parameter Name="Experience" Type="Int32" />
            <asp:Parameter Name="Gender" Type="String" />
            <asp:Parameter Name="DesiredLocation" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Salary" Type="Int32" />
            <asp:Parameter Name="Experience" Type="Int32" />
            <asp:Parameter Name="Gender" Type="String" />
            <asp:Parameter Name="DesiredLocation" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>

</body>
</html>  
