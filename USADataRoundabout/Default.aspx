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
            width: 300px;
        }
    </style>
    <div class="big-flex">
        <div class="flex-container" >
            <dl id="dlSubmittedComps" runat="server">
                <dt>Your Submitted Complaints:</dt>
            </dl>
            <var runat="server" id="submittedComps" style="word-wrap:normal;"></var>
        </div>
        <div class="flex-container">
            <dl id="dlBookmarkedComps" runat="server">
                <dt>Bookmarked Complaints:</dt>
            </dl>
            <var runat="server" id="bookMarkedComps"></var>
        </div>
        <div class="flex-container">
            <dl id="dlCompsSinceLastLogin" runat="server">
                <dt>Complaints Since Last Login:</dt>
            </dl>
            <var runat="server" id="sinceLastLoginComps"></var>
        </div>
    </div>
</asp:Content>
