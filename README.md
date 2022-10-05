# ClimaTempo
<br>

## Proposta
#

Um Aplicativo web para apresentar as previsões do tempo de cidades, mostrando as 3 cidades mais quentes e mais frias de um determinado dia. Mostrar as previsões dos 7 proximos dias da cidade selecionada.  
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
![Preview](imagem/clima_tempo.PNG)


---
Made with 💖 by [**Icarus09**](https://github.com/Icarus09)
