
Cada módulo representa uma tecnologia distinta, mas todas compartilham recursos comuns e podem se comunicar entre si através de endpoints internos.

---

### 🚀 Como Executar

#### Pré-requisitos
1. **Visual Studio** instalado com o **.NET Framework Developer Pack (4.8)**
2. **SQL Server** ou **SQL Server Express** (para conexão com banco de dados)

#### Passos para Executar

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

