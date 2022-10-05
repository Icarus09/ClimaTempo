# ClimaTempo
<br>

## Desafio
#

Desenvolver um app de previsões do tempo de cidades.
<br>
## Tecnologias
#

Tecnologias e ferramentas utilizadas:

- C#
- .NET Framework 4.6.1
- AspNet MVC 5.2.7
- HTML
- CSS
- Bootstrap
- Jquery Select2

<br>


## Configurações para executar o projeto
#
1. Abrir o Visual Studio
2. Alterar a string de conexão no arquivo context para a conxeão com o banco de dados sqlserver (Pasta Models)

```
ClimaTempoSimplesContext.cs

```
3. Executar os comandos de migrations e updade do EF core na aba de desenvolvedor do Nuget.
```
-> add-migration <nome_migration>
-> update-database
```
4. Popular o banco de dados;

4. Executar o projeto.

 
OBS!!!: NÃO POPULAR O BANCO ANTES DE SEGUIR ESSAS ETAPAS.

<br>

## Resultados
#
![Preview](imagem/clima_tempo.png)


---
Made with 💖 by [**Icarus09**](https://github.com/Icarus09)