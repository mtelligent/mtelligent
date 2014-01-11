<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormExperiments._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Web forms Sample</h1>
                <h2>Experiments work in Web Forms</h2>
            </hgroup>
            <asp:Label ID="lblConversionMessage" runat="server" Visible="false"></asp:Label>
            <div>
            <asp:Label ID="lblExperimentHeading" runat="server" Text=""></asp:Label>
            </div>
            <div>
            <asp:Image ID="imgExperiment" runat="server" />
            </div>
            <div>
            <asp:Label ID="lblExperimentMessage" runat="server" Text=""></asp:Label>
            </div>
            <asp:Button ID="btnConvert" runat="server" Text="Button" OnClick="btnConvert_Click" />
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

          <h2>How it Works</h2>
          <p>The code to produce the above result is pretty simple:</p>
          <p><pre>

            &lt;asp:Label ID=&quot;lblConversionMessage&quot; runat=&quot;server&quot; Visible=&quot;false&quot;&gt;&lt;/asp:Label&gt;
            &lt;div&gt;
            &lt;asp:Label ID=&quot;lblExperimentHeading&quot; runat=&quot;server&quot; Text=&quot;&quot;&gt;&lt;/asp:Label&gt;
            &lt;/div&gt;
            &lt;div&gt;
            &lt;asp:Image ID=&quot;imgExperiment&quot; runat=&quot;server&quot; /&gt;
            &lt;/div&gt;
            &lt;div&gt;
            &lt;asp:Label ID=&quot;lblExperimentMessage&quot; runat=&quot;server&quot; Text=&quot;&quot;&gt;&lt;/asp:Label&gt;
            &lt;/div&gt;
            &lt;asp:Button ID=&quot;btnConvert&quot; runat=&quot;server&quot; Text=&quot;Button&quot; OnClick=&quot;btnConvert_Click&quot; /&gt;
              </pre></p>
    <p>
              And then in the code behind:
        </p>
    <p><pre>
        protected void Page_Load(object sender, EventArgs e)
        {

            lblExperimentHeading.Text = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Title"];
            lblExperimentHeading.Font.Bold = true;
            lblExperimentHeading.Style.Add("color:", ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["TitleColor"]);
            lblExperimentMessage.Text = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Copy"];
            imgExperiment.ImageUrl = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Image Source"];
        }

             </pre></p>
          <p>The framework automatically looks up the experiment, assigns a hypothesis and returns any variable your request. On the post, here is the code that registers the conversion and then changes the response method depending on which hypothosis you are associated with.</p>
          <p><pre>
        protected void btnConvert_Click(object sender, EventArgs e)
        {
            ExperimentManager.Current.AddConversion("Honey vs Vinegar");

            if (ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Name == "Honey")
            {
                lblConversionMessage.Text = "Thanks you so much you wonderful user.";
            }
            else
            {
                lblConversionMessage.Text = "We knew you would do as you were told. Carry on worm.";
            }

            lblConversionMessage.Visible = true;
        }
             </pre></p>
</asp:Content>
