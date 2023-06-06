<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" MasterPageFile="/Pages/Store.Master" Inherits="SportsStore.Pages.Listing" %>

<%-- Import web routing --%>
<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
        <div id="content">
            <%
               
                // Itera attraverso ogni oggetto Product nella lista restituita dal metodo GetProducts()
                foreach (WebApplication1.Models.Product prod in GetProducts())
                {
                    // Inizia a scrivere un elemento <div> con la classe CSS 'item'
                    Response.Write("<div class='item'>");

                    // Scrive il nome del prodotto all'interno di un elemento <h3>
                    // Scriviamo {0} perchè è una sintassi di segnaposto utilizzata per sostituire il primo argomento specificato dopo la stringa formattata
                    Response.Write(string.Format("<h3>{0}</h3>", prod.Name));

                    // Scrive la descrizione del prodotto
                    Response.Write(prod.Description);

                    // Scrive il prezzo del prodotto formattato come una rappresentazione di valuta all'interno di un elemento <h4>
                    // Scriviamo "{0:c}" perchè formatta il valore specificato come una rappresentazione di valuta
                    Response.Write(string.Format("<h4>{0:c}</h4>", prod.Price));

                    // Chiude l'elemento <div>
                    Response.Write("</div>");
                }

            %>
        </div>
    <div class="pager">
        <% 
            // Genera i link per la navigazione tra le pagine utilizzando le variabili di routing
            for (int i = 1; i <= MaxPage; i++)
            {
                // Ottiene il percorso virtuale per la pagina corrente
                string path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary() { { "page", i } }).VirtualPath;
    
                // Scrive il link HTML per la pagina corrente, evidenziando il link della pagina attuale
                Response.Write(
                    string.Format("<a href='{0}' {1}>{2}</a>",
                    path, i == CurrentPage ? "class='selected'" : "", i));
            }

        %>
       
    </div>
</asp:Content>

<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    Page Footer
</asp:Content>
