<%@ Page Title="انتقال وجه" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TransCashCallBack.cs" Inherits="BMI_TransCashCallBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="page-title">
        نتیجه پرداخت
    </div>
    <br />
    <p class="alarm">
        <asp:Label ID="lblMessage" runat="server" CssClass="lbl-msg" EnableViewState="false" />
    </p>
</asp:Content>

