# Character
ゲーム内に登場する各キャラクターの処理を書いています。

## CharaBase.cs
このスクリプトを継承する形で各キャラを実装しています。

コードを書き始めた当時は継承を利用せずに実装していたのですが、途中で継承を利用する方針に変更しています。(移行は完全に完了していません。)

そのため、継承を利用していなかった時代のスクリプトもPlayerBase.csという形で残っています。

そちらはかなり冗長な作りになっており、改善策として継承を取り入れたという経緯があります。