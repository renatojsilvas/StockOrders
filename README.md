# StockOrders

# Objetivos
Simular a execução de ordens semelhantes à bolsa de valores em um aplicativo WPF

# Ferramentas
- Visual Studio 2022
- .NET 6
- XUnit
- Moq
- Moq.AutoMocker
- Análise de Desempenho do Visual Studio 2022
- Coverlet
- Fluent Assertions

# Testes
Foram realizados testes unitários utilizando o framework Xunit. A cobertura dos testes foi superior a 95%. Para mais detalhes baixar o relatório completo no Release da versão V1.0 (CoberturaDeTestes.html)


# Análises
Foi realizada uma análise de desempenho da aplicação utilizando a própria ferramenta do Visual Studio 2022. A simulação roda em baixa carga até os 10 segundos. Entre os 10 segundos e 20 segundos, a carga de ordens aumenta, baixando novamente em torno dos 20 segundos. Não foi notado nenhuma variação sensível nem no consumo de recursos nem na produtividade visual, mentendo razoável atualização da tela. Para mais detalhes, baixar a análise de desempenho no Release da versão V1.0. (AnaliseDePerformance.diagsession)



