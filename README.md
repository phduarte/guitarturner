# GuitarTurner

Afinador de guitarra em WPF com C# e .NET 5

![.NET Core Desktop](https://github.com/phduarte/guitarturner/workflows/.NET%20Core%20Desktop/badge.svg) [![CodeFactor](https://www.codefactor.io/repository/github/phduarte/guitarturner/badge)](https://www.codefactor.io/repository/github/phduarte/guitarturner)

![arquivo](screen.png)

## Listen and Detect Pitch

O princípio desse afinador é simples que contém apenas 2 componentes que executam os seguintes papeis, um ouvinte e um analisador de frequências.

### SoundListner

O monitor `SoundListner`, é responsável por ouvir todas as entradas sonoras de uma entrada de áudio do computador.

Algumas características dessa classe incluem:

- É uma classe facade (ACL)
- Possui um método assíncrono que fica rodando até que a aplicação pare.
- Utiliza a biblioteca NAudio para transcrever as ondas sonoras em stream de bytes.
- Utiliza um analisador de frequências para detectar notas coincidentes com a afinação de uma guitarra.

### PitchDetector

Basicamente, é o responsável por encontrar frequências dentro de amostras sonoras.

Suas características são:

- Realiza a leitura da amostra de som.
- Analisa os valores da amostra a fim de detectar qual a frequência.

## Requisitos

- .NET 5
- [NAudio 1.10+](https://github.com/naudio/NAudio) 

## Referencias

Agradecimentos especiais à biblioteca NAudio que tornou possível esse projeto.