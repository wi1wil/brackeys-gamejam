<!-- GIFs showcase -->
<table width="100%">
  <tr>
    <!-- Top large gif -->
    <td colspan="2" align="center">
      <img src="https://github.com/yourusername/BellyBetCookies/raw/main/bbc1.gif" width="100%"/>
    </td>
  </tr>
  <tr>
    <!-- Bottom two gifs -->
    <td align="center" width="50%">
      <img src="https://github.com/yourusername/BellyBetCookies/raw/main/bbc2.gif" width="100%"/>
    </td>
    <td align="center" width="50%">
      <img src="https://github.com/yourusername/BellyBetCookies/raw/main/bbc3.gif" width="100%"/>
    </td>
  </tr>
</table>

---

<!-- About the game -->
<table width="100%">
  <tr>
    <!-- Left Image -->
    <td width="30%" align="center" valign="middle" style="padding:15px;">
      <img src="https://github.com/yourusername/BellyBetCookies/raw/main/logo.png" width="220"/>
    </td>
    <!-- Right Text -->
    <td width="70%" valign="top" style="padding:15px;">
      <h2>🍪 About </h2>
      <p style="max-width:700px;">
        Belly Bet Cookies is a survival gambling game where every cookie is both a blessing and a curse. 
        Eat cookies to score points, but risk obesity that slows you down. Save cookies to gamble at slot machines or trade them for revives. 
        Escape the kitchen before you collapse under your own weight!
      </p>
      <a href="https://kangmantul.itch.io/rat-the-cookies-maniac">
        <img src="https://img.shields.io/badge/Itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white" />
      </a>
    </td>
  </tr>
</table>

<h2>📜 Scripts</h2>
<table>
  <tr>
    <th>Script</th>
    <th>Description</th>
  </tr>

  <tr>
    <td><code>PlayerController.cs</code></td>
    <td>Handles player movement, eating cookies, and interactions with exits or slot machines.</td>
  </tr>
  <tr>
    <td><code>EnemyChase.cs</code></td>
    <td>Controls enemy AI that hunts the player throughout the kitchen.</td>
  </tr>
  <tr>
    <td><code>CookieManager.cs</code></td>
    <td>Manages spawning and tracking of cookies, including eaten and saved cookies.</td>
  </tr>
  <tr>
    <td><code>ObesitySystem.cs</code></td>
    <td>Implements the obesity mechanic, adjusting player stats as they eat more cookies.</td>
  </tr>
  <tr>
    <td><code>SlotMachine.cs</code></td>
    <td>Controls the slot machine gamble, including boosts, revives, or debuffs outcomes.</td>
  </tr>
</table>

---

<pre>
BellyBetCookies                   # Root folder of the project
└── Assets                        # Default Unity folder for all game assets, scripts, and scenes
    ├── Audio                     # Stores BGM and SFX
    ├── Fonts                     # Stores game fonts
    ├── Materials                 # Stores material assets
    ├── Prefabs                   # Stores pre-configured game objects
    ├── Scenes                    # All Unity scenes
    ├── Scripts                   # All C# scripts
    │   ├── PlayerController.cs
    │   ├── EnemyChase.cs
    │   ├── CookieManager.cs
    │   ├── ObesitySystem.cs
    │   ├── SlotMachine.cs
    │   ├── ExitManager.cs
    │   ├── UIManager.cs
    │   ├── PowerUpManager.cs
    │   ├── GameManager.cs
    │   └── ArrowGuide.cs
    ├── Sprites                   # Stores 2D art assets
    └── UI                        # UI elements and prefabs
</pre>

---

<h2 align="left">✨ Features</h2>

<ul>
  <li><b>🍪 Cookie Dilemma</b><br/>Every cookie is both score and risk. Eat it for instant points, or save it for gambling/trading. The more you eat, the heavier and slower you become.</li>
  <li><b>⚖️ Obesity System (4 Levels)</b><br/>Eating too much raises your Obesity Level. Higher levels = slower movement, reduced dodging, increased chance of death.</li>
  <li><b>🎰 Slot Machine Gamble</b><br/>Cookies can be used to spin a slot machine. High risk, high reward: gain boosts, revives, or crippling debuffs.</li>
  <li><b>💔 Insurance Revive System (“Lonte Trade”)</b><br/>Trade some cookies for life insurance :)</li>
</ul>

---

<h2>📋 Developers & Contributions</h2>
<table>
  <tr>
    <td align="center" width="120">
      <img src="https://github.com/wi1wil.png" width="80" style="border-radius:50%;" alt="Wilson"/>
    </td>
    <td align="left">
      <b>Wilson H.</b><br/>
      <sub>Lead Programmer</sub><br/>
      <p style="margin:0;">Implemented core gameplay, and all the mechanics.</p>
    </td>
  </tr>
  <tr>
    <td align="center" width="120">
      <img src="https://github.com/kangmantul.png" width="80" style="border-radius:50%;" alt="Jordy"/>
    </td>
    <td align="left">
      <b>Jordy T.</b><br/>
      <sub>Game Designer</sub><br/>
      <p style="margin:0;">Worked on designing the gameplay, and the core mechanics of the game.</p>
    </td>
  </tr>
  <tr>
    <td align="center" width="120">
      <img src="https://github.com/ChocoMOCC.png" width="80" style="border-radius:50%;" alt="Kelvin"/>
    </td>
    <td align="left">
      <b>Kelvin</b><br/>
      <sub>Lead Game Artist</sub><br/>
      <p style="margin:0;">Created character sprites, UI, and kitchen environment art.</p>
    </td>
  </tr>
</table>

---

<h2>🎮 Controls / Inputs</h2>
<table>
  <tr>
    <th>Action</th>
    <th>Key / Input</th>
  </tr>
  <tr>
    <td>Move Up</td>
    <td><b>W</b></td>
  </tr>
  <tr>
    <td>Move Left</td>
    <td><b>A</b></td>
  </tr>
  <tr>
    <td>Move Down</td>
    <td><b>S</b></td>
  </tr>
  <tr>
    <td>Move Right</td>
    <td><b>D</b></td>
  </tr>
  <tr>
    <td>Eat Cookies</td>
    <td><b>Left Mouse Button</b></td>
  </tr>
  <tr>
    <td>Save Cookies (currency)</td>
    <td><b>Right Mouse Button</b></td>
  </tr>
  <tr>
    <td>Interact (Slot / Exit)</td>
    <td><b>E</b></td>
  </tr>
  <tr>
    <td>Use Power Up 1</td>
    <td><b>Q</b></td>
  </tr>
  <tr>
    <td>Use Power Up 2</td>
    <td><b>Z</b></td>
  </tr>
  <tr>
    <td>Use Power Up 3</td>
    <td><b>F</b></td>
  </tr>
  <tr>
    <td>Use Power Up 4</td>
    <td><b>X</b></td>
  </tr>
</table>

<hr/>
<!-- Credits -->
<td width="50%" valign="top">
  <h2 align="left">🎵 Some assets taken!</h2>
  <p align="left">
    <a href="https://pixabay.com/">
      <img src="https://img.shields.io/badge/Assets-Pixabay-2EC866?style=for-the-badge&logo=pixabay&logoColor=white" />
    </a>
    <a href="https://www.behance.net/gallery/109682665/Handdrawn-Pixel-Font-FREE">
      <img src="https://img.shields.io/badge/Font-Behance-1769FF?style=for-the-badge&logo=behance&logoColor=white" />
    </a>
  </p>
</td>
