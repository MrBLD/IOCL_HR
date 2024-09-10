<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs"
    Inherits="IOCL_Internship_2nd.NewFolder1.WebForm1" Async="true" %>
    <!DOCTYPE html>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

<script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.Event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
    }
    $(submitButton).click(function () {
        // For example, in the click event of your button
        Event.preventDefault(); // Stop the default action, which is to post
    });

</script>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>IOCL@AI</title>
        <link rel="stylesheet" href="styles.css" />
        <script src="script.js"></script>
    </head>

    <body style="font-family: sans-serif;">
        <div style="display: flex;">
            <div style="font-size: 45px; font-weight: 600;">IOCL</div>
            <div style="display: flex; justify-content: flex-end; position: absolute; right: 5vb; font-size: larger;">
                <div class="menu-button">About</div>
                <div class="menu-button">Albums</div>
                <div class="menu-button">Contact</div>
                <div class="menu-button">FB</div>
                <div class="menu-button">Insta</div>
            </div>
        </div>

        <div style="font-size: 3vw; padding-left: 4vw; padding-top: 4vw; -webkit-text-stroke-width: 1px; -webkit-text-stroke-color: darkorange;"><span
                style=" font-weight: bold; font-size: 6vw;">IOCL Barauni,</span> </br> Artificial Intelligece HR
            Chatbot
        </div>

        <button id="openBotBtn" onclick="toggleBot()">Open Bot</button>
        <form class="chat-container" id="chatContainer" runat="server">
            <div class="chat-header">
                <span id="closeBotBtn" onclick="toggleBot()">←</span>
            </div>
            <div class="chat-box" id="chatBox">
                <div>
                    <label runat="server" class="result" id="resultlabel">Hi, how may I help You?</label>
                </div>
                <div style="padding:2px; display:flex; flex-wrap:nowrap">
                    <!-- <label for="userInput">Enter Input:</label> -->
                    <asp:TextBox ID="userInput" runat="server" placeholder="Ask Something"></asp:TextBox>
                    <br />
                    <div style="display: flex; align-items: center; padding:4px">
                        <asp:Button ID="submitButton"
                            style="border-radius:6px; background-color: darkorange; color: #fff; border: none; cursor: pointer;"
                            runat="server" Text="Send " OnClick="SubmitButton_Click" CssClass="submit-button"
                            class="fa fa-paper-plane" />
                    </div>
                </div>
                <br />
            </div>
        </form>

    </body>

    </html>