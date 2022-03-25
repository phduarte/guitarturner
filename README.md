# GuitarTurner

Afinador de guitarra em WPF com C# e .NET 5

![.NET Core Desktop](https://github.com/phduarte/guitarturner/workflows/.NET%20Core%20Desktop/badge.svg) [![CodeFactor](https://www.codefactor.io/repository/github/phduarte/guitarturner/badge)](https://www.codefactor.io/repository/github/phduarte/guitarturner)

![arquivo](screen.png)

## Listen and Detect Pitch

O princípio desse afinador é simples, ele contém apenas 2 componentes, um ouvinte e um analisador de frequências.

### SoundListner

O `SoundListner` é responsável por ouvir todos os sinais sonoros de uma determinada entrada de áudio do computador.

Algumas características dessa classe incluem:

- É uma classe facade (ACL)
- Utiliza a biblioteca NAudio para transcrever as ondas sonoras em stream de bytes.
- Possui um método assíncrono que fica rodando em loop enquanto a aplicação estiver em execução.
- Através de um analisador de frequências, detecta se os sinais sonoros correspondem a uma notas musical.

### PitchDetector

É o responsável por encontrar frequências dentro de amostras sonoras.

Suas características são:

- Realiza a leitura da amostra de som.
- Analisa os valores da amostra a fim de detectar qual a frequência.

## Requisitos

- .NET 5
- [NAudio 1.10+](https://github.com/naudio/NAudio) 

## Referencias

Agradecimentos especiais à biblioteca NAudio que tornou possível esse projeto.
