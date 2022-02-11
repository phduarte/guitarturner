# GuitarTurner

Afinador de guitarra em WPF com C# e .NET 5

![.NET Core Desktop](https://github.com/phduarte/guitarturner/workflows/.NET%20Core%20Desktop/badge.svg) [![CodeFactor](https://www.codefactor.io/repository/github/phduarte/guitarturner/badge)](https://www.codefactor.io/repository/github/phduarte/guitarturner)

![arquivo](screen.png)

## Listen and Detect Pitch

O princ�pio desse afinador � simples que cont�m apenas 2 componentes que executam os seguintes papeis, um ouvinte e um analisador de frequ�ncias.

### SoundListner

O monitor `SoundListner`, � respons�vel por ouvir todas as entradas sonoras de uma entrada de �udio do computador.

Algumas caracter�sticas dessa classe incluem:

- � uma classe facade (ACL)
- Possui um m�todo ass�ncrono que fica rodando at� que a aplica��o pare.
- Utiliza a biblioteca NAudio para transcrever as ondas sonoras em stream de bytes.
- Utiliza um analisador de frequ�ncias para detectar notas coincidentes com a afina��o de uma guitarra.

### PitchDetector

Basicamente, � o respons�vel por encontrar frequ�ncias dentro de amostras sonoras.

Suas caracter�sticas s�o:

- Realiza a leitura da amostra de som.
- Analisa os valores da amostra a fim de detectar qual a frequ�ncia.

## Requisitos

- .NET 5
- [NAudio 1.10+](https://github.com/naudio/NAudio) 

## Referencias

Agradecimentos especiais � biblioteca NAudio que tornou poss�vel esse projeto.