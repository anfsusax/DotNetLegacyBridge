# GTI2 - Sistema de Gestão

## 📋 Visão Geral

O GTI2 é um sistema de gestão empresarial desenvolvido em .NET Framework que integra diferentes tecnologias e padrões de arquitetura. O projeto combina WebForms legado com APIs modernas, seguindo uma arquitetura em camadas para separação de responsabilidades.

## 🏗️ Estrutura do Projeto

O projeto está organizado nas seguintes camadas e componentes principais:

- **WebForms**: Aplicação Web Forms legada
- **Wcf**: Serviços WCF para integração
- **BL**: Camada de negócios
- **DAO**: Camada de acesso a dados
- **Model**: Modelos de domínio
- **API**: API Web moderna (Web API)
- **BTI.MvcX**: Componentes MVC

## 🚀 Pré-requisitos

- **Visual Studio 2019 ou superior**
- **.NET Framework 4.7.2**
- **SQL Server 2016 ou superior** (ou SQL Server Express)
- **IIS Express** (incluído no Visual Studio)
- **Gerenciador de Pacotes NuGet**

## ⚙️ Configuração do Ambiente

1. **Banco de Dados**:
   - Restaure o banco de dados a partir do diretório `Sql/`
   - Atualize a string de conexão em `API/Web.config` e `GTI.Wcf/Web.config`

2. **Configuração da Solução**:
   - Abra o arquivo `GTI2.sln` no Visual Studio
   - Restaure os pacotes NuGet (o Visual Studio fará isso automaticamente)
   - Defina múltiplos projetos de inicialização para `GTI.WebForms` e `API`

## 🛠️ Executando o Projeto

1. Abra a solução no Visual Studio
2. Pressione F5 ou clique em "Iniciar Depuração"
3. O navegador padrão abrirá com a aplicação WebForms
4. A API estará disponível em `http://localhost:[porta]/api`

## 🔧 Desenvolvimento

### Tecnologias Principais

- **Frontend**: ASP.NET WebForms, jQuery, Bootstrap
- **Backend**: .NET Framework 4.7.2, C#
- **APIs**: ASP.NET Web API, WCF
- **Banco de Dados**: SQL Server
- **ORM**: Entity Framework 5.0

### Convenções de Código

- Use nomes descritivos para variáveis e métodos
- Comente o código quando necessário para explicar lógicas complexas
- Siga o padrão de nomenclatura do C# (PascalCase para métodos e classes, camelCase para variáveis locais)

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`)
3. Commit suas alterações (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

### 🚀 Passos para Execução

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd gti
   ```

2. **Restaure os pacotes NuGet**
   ```bash
   # Via NuGet CLI
   nuget restore GTI2.sln
   
   # Ou via Visual Studio
   # Clique com botão direito na solução > Restore NuGet Packages
   ```

3. **IMPORTANTE: Problema do Roslyn (csc.exe)**
   
   Se você encontrar o erro: **"não foi possível localizar uma parte do caminho c:\...\roslyn\csc.exe"**, siga estes passos:
   
   - O pacote `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` já está configurado
   - Os arquivos do Roslyn são gerados automaticamente durante o build
   - **NÃO** inclua os arquivos da pasta `bin\roslyn\` no controle de versão
   - Execute o build do projeto (F6 ou Build > Build Solution)
   - O NuGet copiará automaticamente os arquivos necessários para `bin\roslyn\`

4. **Configure a Connection String**
   
   Edite o `Web.config` e ajuste a connection string conforme seu ambiente:
   ```xml
   <connectionStrings>
     <add name="conAlex" connectionString="Data Source=SEU_SERVIDOR;Initial Catalog=GTI;Integrated Security=True;" />
   </connectionStrings>
   ```

5. **Execute o projeto**
   - Abra a solução `GTI2.sln` no Visual Studio
   - Defina o projeto inicial desejado (ex: `API`, `BTI.MvcX`, `GET.WebForms`)
   - Pressione **F5** para executar  

---

### 📚 Propósito
Este projeto não tem foco em modernização, mas em **compreender e preservar o funcionamento das bases legadas .NET**, algo ainda muito presente em grandes sistemas corporativos.  

É um repositório de aprendizado, diagnóstico e documentação viva de um ecossistema que moldou gerações de desenvolvedores .NET.  

---

### 🧠 Autor
Desenvolvido por **Alex Feitoza**  
💻 *“Compreender o legado é o primeiro passo para construir o futuro.”*

---

