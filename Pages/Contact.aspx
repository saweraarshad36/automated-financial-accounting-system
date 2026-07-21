<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Pages_Contact" %>

<%@ Register src="../User Controls/ContactUc.ascx" tagname="ContactUc" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        body {
            background: url('../Images/imp.jpg') no-repeat center center fixed;
            background-size: cover;
            margin: 0;
            padding: 0;
            min-height: 100vh;
        }
    </style>
    <uc1:ContactUc ID="ContactUc" runat="server" />
</asp:Content>
    