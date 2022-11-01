# BattleScene
対戦画面の処理を書いています。
大部分はUI関連のスクリプトが多いです。

## BattleManager.cs
使用キャラの召喚を初めとするゲームの総合管理を書いています。
勝利判定や死亡判定などのキャラに関するゲーム処理はCharacter/CharaBase.csに書いています。
基本的に、BattleManager.csとCharaBase.csでゲームの大きなシステムを形作っています。
