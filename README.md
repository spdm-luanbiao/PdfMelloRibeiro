# 📄 PDF2XLS - Conversor de Relatórios MELLO RIBEIRO

## 🩺 1. Qual problema este sistema resolve

Este sistema foi desenvolvido especificamente para atender uma necessidade pontual da MELLO RIBEIRO: converter relatórios em PDF para Excel de forma rápida e confiável. O processo manual de leitura e digitação desses dados é demorado, repetitivo e sujeito a erros. A aplicação elimina esse esforço operacional, garantindo precisão e agilidade na extração das informações.

## ⚙️ 2. Como o sistema funciona

A aplicação realiza a leitura do conteúdo textual dos PDFs e utiliza expressões regulares (Regex) ajustadas ao padrão específico dos relatórios da MELLO RIBEIRO para identificar e estruturar duas tabelas: uma detalhada e um resumo. Em seguida, os dados são organizados em objetos estruturados e exportados para Excel utilizando a biblioteca ClosedXML, já com formatação adequada e cálculos automáticos aplicados.

## 🚀 3. Como utilizar a aplicação

Para utilizar, selecione a pasta contendo os arquivos PDF fornecidos no padrão esperado, escolha um arquivo na lista e clique em "Processar" para visualizar os dados extraídos. Após a conferência, é possível exportar o resultado individualmente ou em lote para Excel. Em caso de dúvida sobre o formato correto do PDF, utilize o botão "Modelo" para abrir um exemplo de referência. A aplicação foi construída para uso direto, simples e objetivo, sem necessidade de configurações adicionais.