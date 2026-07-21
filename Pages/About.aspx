<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Pages_About" %>

<%@ Register src="../User Controls/AboutUc.ascx" tagname="AboutUc" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:AboutUc ID="AboutUc1" runat="server" />
</asp:Content>

