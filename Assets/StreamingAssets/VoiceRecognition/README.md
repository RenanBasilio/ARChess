Para executar o projeto com reconhecimento de voz, coloque os seguintes arquivos nessa pasta, segundo a estrutura de pastas definida abaixo:

```
Voice Recognition
    |
    |---- LanguageModel
    |       |
    |        ---- en-70-0.1.lm.bin
     ---- VoiceModel
            |
             ---- cmusphinx-en-us-ptm-8khz-5.2
                        |
                        |---- fear.params
                        |---- mdef
                        |---- means
                        |---- mixture_weights
                        |---- noisedict
                        |---- sendump
                        |---- transition_matrices
                         ---- variances
```

Os arquivos podem ser obtidos do [site oficial do CMUSphinx](https://cmusphinx.github.io/wiki/download/#models).