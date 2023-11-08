<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="USADataRoundabout._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .big-flex {
            display: flex;
            flex-direction: row;
            justify-content: space-evenly;
            padding-top: 50px;
        }
        .flex-container {
            display: flex;
            flex-direction: column;
            align-items: start;
            border: solid;
            border-radius: 13px;
            padding: 10px;
        }
    </style>
    <div class="big-flex">
        <div class="flex-container">
            <p>Your Submitted Complaints</p>
        </div>
        <div class="flex-container">
            <p>Bookmarked Complaints</p>
        </div>
        <div class="flex-container">
            <p>Complaints Since Last Login</p>
        </div>
    </div>

</asp:Content>
