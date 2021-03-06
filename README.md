###### fireice-uk's and psychocrypt's
# XMR-Stak - Cryptonight All-in-One Mining Software
# Wasabi edition

XMR-Stak is a universal Stratum pool miner. This miner supports CPUs, AMD and NVIDIA gpus and can be used to mine the crypto currencys Monero, Aeon and many more Cryptonight coins.
Our wasabi edition is a custom xmr-stak version with more features and improvements.

## HTML reports
<img src="https://i.redd.it/aqdq1lnkrz711.png"><img src="https://i.redd.it/1v8lndvlrz711.png">

## Video setup guide on Windows

[<img src="https://gist.githubusercontent.com/fireice-uk/3621b179d56f57a8ead6303d8e415cf6/raw/f572faba67cc9418116f3c1dfd7783baf52182ce/vidguidetmb.jpg">](https://youtu.be/YNMa8NplWus)
###### Video by Crypto Sewer

## Overview
* [Features](#features)
* [Supported altcoins](#supported-altcoins)
* [Download](#download)
* [Usage](doc/usage.md)
* [FAQ](doc/FAQ.md)
* [Developer Donation](#default-developer-donation)


## Features

- support all common backends (CPU/x86, AMD-GPU and NVIDIA-GPU)
- support all common OS (Linux, Windows and macOS)
- supports algorithm cryptonight for Monero (XMR) and cryptonight-light (AEON)
- easy to use
  - guided start (no need to edit a config file for the first start)
  - auto configuration for each backend
- open source software (GPLv3)
- TLS support
- [HTML statistics](doc/usage.md#html-and-json-api-report-configuraton)
- [JSON API for monitoring](doc/usage.md#html-and-json-api-report-configuraton)
- hardware monitoring features
- mining extimations

## Supported altcoins

Besides [Monero](https://getmonero.org), following coins can be mined using this miner:

- [Aeon](http://www.aeon.cash)
- [BBSCoin](https://www.bbscoin.xyz)
- [BitTube](https://coin.bit.tube/)
- [Graft](https://www.graft.network)
- [Haven](https://havenprotocol.com)
- [Intense](https://intensecoin.com)
- [Masari](https://getmasari.org)
- [Ryo](https://ryo-currency.com)
- [TurtleCoin](https://turtlecoin.lol)

If your prefered coin is not listed, you can chose one of the following algorithms:

- 1MiB scratchpad memory
    - cryptonight_lite
    - cryptonight_lite_v7
    - cryptonight_lite_v7_xor (algorithm used by ipbc)
- 2MiB scratchpad memory
    - cryptonight
    - cryptonight_masari
    - cryptonight_v7
    - cryptonight_v7_stellite
- 4MiB scratchpad memory
    - cryptonight_haven
    - cryptonight_heavy

Please note, this list is not complete, and is not an endorsement.

## Download

You can find the latest releases and precompiled binaries on GitHub under [Releases](https://github.com/wasabidevs/xmr-stak/releases/).

## Default Developer Donation

By default the miner will donate 1% of the hashpower (1 minute in 100 minutes) to my pool. If you want to change that, edit [donate-level.hpp](xmrstak/donate-level.hpp) before you build the binaries.

If you want to donate directly to support further development, here is my wallet

wasabi devs:
```
4ADFnuC1kDQGtkncbETsHnTt7qmQFvwjUMn7Awvh3GAZMRuSaGqTo2j9K2wW4qjpMEQCDPzJmhr2cXEKJqUe9uJvBrdeD3o
```

fireice-uk:
```
4581HhZkQHgZrZjKeCfCJxZff9E3xCgHGF25zABZz7oR71TnbbgiS7sK9jveE6Dx6uMs2LwszDuvQJgRZQotdpHt1fTdDhk
```

psychocrypt:
```
45tcqnJMgd3VqeTznNotiNj4G9PQoK67TGRiHyj6EYSZ31NUbAfs9XdiU5squmZb717iHJLxZv3KfEw8jCYGL5wa19yrVCn
```
