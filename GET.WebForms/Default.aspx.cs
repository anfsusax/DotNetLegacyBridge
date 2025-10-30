using GTI.API.Models;
using GTI.Wcf;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GET.WebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarClientes();
                CarregarDropDownLists();
            }
        }

        private void CarregarClientes()
        {
            var serviceCliente = new ServiceCliente();
            var clientes = serviceCliente.Listar();

            gdvClientes.DataSource = clientes;
            gdvClientes.DataBind();
        }

        private void CarregarDropDownLists()
        {
            // Sexo
            ddlSexo.Items.Clear();
            ddlSexo.Items.Add(new ListItem("Selecione", ""));
            ddlSexo.Items.Add(new ListItem("Masculino", "Masculino"));
            ddlSexo.Items.Add(new ListItem("Feminino", "Feminino"));
            ddlSexo.Items.Add(new ListItem("Outro", "Outro"));

            // Estado civil
            ddlEstadoCivil.Items.Clear();
            ddlEstadoCivil.Items.Add(new ListItem("Selecione", ""));
            ddlEstadoCivil.Items.Add(new ListItem("Solteiro(a)", "Solteiro(a)"));
            ddlEstadoCivil.Items.Add(new ListItem("Casado(a)", "Casado(a)"));
            ddlEstadoCivil.Items.Add(new ListItem("Divorciado(a)", "Divorciado(a)"));
            ddlEstadoCivil.Items.Add(new ListItem("Viúvo(a)", "Viúvo(a)"));

            // UF
            ddlUf.Items.Clear();
            ddlUf.Items.Add(new ListItem("Selecione", ""));
            ddlUf.Items.Add(new ListItem("SP", "SP"));
            ddlUf.Items.Add(new ListItem("RJ", "RJ"));
            ddlUf.Items.Add(new ListItem("MG", "MG"));
            ddlUf.Items.Add(new ListItem("BA", "BA"));
            ddlUf.Items.Add(new ListItem("RS", "RS"));
            ddlUf.Items.Add(new ListItem("PR", "PR"));
        }

        public int Id
        {
            get => ViewState["Id"] != null ? (int)ViewState["Id"] : 0;
            set => ViewState["Id"] = value;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var ctiBr = new CultureInfo("pt-BR");
            var cliente = new Cliente();

            cliente.Id = Id;
            cliente.Nome = txtNome.Text.Trim();
            cliente.Cpf = txtCpf.Text.Trim();
            cliente.Rg = txtRg.Text.Trim();
            cliente.Sexo = ddlSexo.SelectedValue;
            cliente.EstadoCivil = ddlEstadoCivil.SelectedValue;

            // Data de nascimento
            if (DateTime.TryParse(txtDataNascimento.Text, ctiBr, DateTimeStyles.None, out DateTime dataNasc))
                cliente.DataNascimento = dataNasc;
            else
                cliente.DataNascimento = DateTime.MinValue;

            // Endereço
            cliente.Cep = txtCep.Text.Trim();
            cliente.Logradouro = txtRua.Text.Trim();
            cliente.Numero = txtNumero.Text.Trim();
            cliente.Bairro = txtBairro.Text.Trim();
            cliente.Uf = ddlUf.SelectedValue;

            var serviceCliente = new ServiceCliente();

            try
            {
                if (cliente.Id == 0)
                {
                    serviceCliente.Incluir(cliente);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Cliente cadastrado com sucesso!');", true);
                }
                else
                {
                    serviceCliente.Alterar(cliente);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Cliente atualizado com sucesso!');", true);
                }

                LimparCampos();
                CarregarClientes();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "erro", $"alert('Erro ao salvar: {ex.Message}');", true);
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            ddlSexo.SelectedIndex = 0;
            ddlEstadoCivil.SelectedIndex = 0;
            txtDataNascimento.Text = "";
            txtCep.Text = "";
            txtRua.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            ddlUf.SelectedIndex = 0;
            Id = 0;
        }

        protected void gdvClientes_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            var serviceCliente = new ServiceCliente();
            var ctiBr = new CultureInfo("pt-BR");

            if (e.CommandName == "Editar")
            {
                Id = Convert.ToInt32(e.CommandArgument);
                var cliente = serviceCliente.Obter(Id);

                if (cliente != null)
                {
                    txtNome.Text = cliente.Nome;
                    txtCpf.Text = cliente.Cpf;
                    txtRg.Text = cliente.Rg;

                    // Garantir itens antes de selecionar valores vindos do banco
                    EnsureListItem(ddlSexo, cliente.Sexo);
                    EnsureListItem(ddlEstadoCivil, cliente.EstadoCivil);
                    EnsureListItem(ddlUf, cliente.Uf);

                    ddlSexo.SelectedValue = cliente.Sexo ?? string.Empty;
                    ddlEstadoCivil.SelectedValue = cliente.EstadoCivil ?? string.Empty;
                    txtDataNascimento.Text = cliente.DataNascimento != DateTime.MinValue ? cliente.DataNascimento.ToString("yyyy-MM-dd") : string.Empty;
                    txtCep.Text = cliente.Cep;
                    txtRua.Text = cliente.Logradouro;
                    txtNumero.Text = cliente.Numero;
                    txtBairro.Text = cliente.Bairro;
                    ddlUf.SelectedValue = cliente.Uf ?? string.Empty;

                    // Abrir modal de edição no front-end
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openClienteModal", "var m=new bootstrap.Modal(document.getElementById('clienteModal'));m.show();", true);
                }
            }

            if (e.CommandName == "Excluir")
            {
                Id = Convert.ToInt32(e.CommandArgument);
                serviceCliente.Excluir(Id);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Cliente excluído com sucesso!');", true);
                CarregarClientes();
            }
        }

        private static void EnsureListItem(ListControl list, string value)
        {
            if (list == null) return;
            var val = value ?? string.Empty;
            if (list.Items.FindByValue(val) == null)
            {
                list.Items.Add(new ListItem(val, val));
            }
        }
    }
}
