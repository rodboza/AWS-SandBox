# AWS-Sandbox (v1.0)


Criar um espaço para testar algumas features da AWS e DEVOPS


Para execução dessa sandbox são necessários os seguinte itens:
- Ter um conta no site [OpenWeather](https://openweathermap.org/)
    - Solicitar uma API ID
- Ter uma conta AWS;
- Criar uma Secret no AWS Secrets Manager
    - Nome: **openweather**
    - Secret Key **apiid**
    - Secret Value **API ID**
- Criar uma rule no AWS EventBridge
    - Apontar para Lambda	fn-get-weather-info
    - passar como parametro **Constant (JSON text) {"local":"Santos"}**
- Fazer as configurações no IAM


Abaixo os comandos para configurara a estação de trabalho com as suas credencias:


No terminal (windows) digite:
``` sh
setx AWS_ACCESS_KEY_ID "COLE-AQUI-SUA-ACCESS-KEY-ID"
setx AWS_SECRET_ACCESS_KEY "COLE-AQUI-SUA-ACCESS-KEY-SECRET"
setx AWS_REGION "COLE-AQUI-SUA-AWS-REGION"
```

No terminal (windows) digite:
``` sh
git clone https://github.com/rodboza/aws-sandbox.git
git checkout v1.0
cd aws-sandbox
dotnet test
```


# Desenho da Arquitetura:<p>
![Desenho da Arquitetura](https://raw.githubusercontent.com/rodboza/aws-sandbox/main/DesenhoArquitetura.png)
 
# Change log:<p>
| Versão | Descrição |
| ---    | :---      |
| v1.0 | Versão inicial  |

