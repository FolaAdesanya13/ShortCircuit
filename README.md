# ⚡ SHORT-CIRCUIT

<div align="center">

[![Unity Version](https://img.shields.io/badge/Unity-6000.3-black?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/Code-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)

**A 3D First-Person Shooter prototype focused on runtime performance optimization, clean component-based architecture, and efficient state management in Unity.** (work in progress)

</div>

---

## 🎬 Gameplay Preview
<div align="center">
  <img width="400" height="250" alt="ShortCircuit_DemoGameplay (2)" src="https://github.com/user-attachments/assets/417de846-ba24-4800-b57c-7c938caf2fdb" />
</div>

---
## ⚙️ Gameplay Engineering & Architecture

**Short-Circuit** is built to explore how performance patterns and modular software design can prevent common bottlenecks in rapid indie game prototyping.

### 1. Zero-Allocation Object Pooling
* **The Problem:** Repeatedly instantiating and destroying enemy GameObjects at runtime triggers heavy Garbage Collection spikes, causing micro-stutters and frame drops during intense combat waves.
* **Instance of Solution:** Implemented an object pool pattern. Enemies are pre-allocated into a fixed memory pool during scene initialization. Upon defeat, entities are deactivated rather than destroyed—stripping them from active calculations and are later recycled and repositioned when respawning.

### 2. Component-Based Architecture & Composition
* **The Problem:** Writing monolithic scripts that handle movement, combat, and spawning all in one place creates tight coupling and hard to debug spaghetti code.
* **Instance of Solution:** Adopted strict component composition. Global spawner coordination is isolated on a dedicated manager object, while individual enemy entities manage their local behavior. This modularity ensures clean separation of concerns.

### 3. Asynchronous Scene Loading & State Guards
* **The Problem:** Synchronous scene transitions freeze the main thread, while un-gated checks inside high-frequency update loops risk firing redundant load commands.
* **Instance of Solution:** Leveraged non-blocking asynchronous loading coupled with boolean execution locks to ensure scene transitions safely fire precisely once without stalling the engine.

### 4. Zero-Allocation Spatial Proximity & Range Detection
* **The Problem:** Continuously evaluating distance checks or unoptimized sphere queries inside high-frequency update loops creates unnecessary CPU overhead, while attempting to offload Unity physics or transform queries to background threads fails due to strict single-threaded API restrictions.
* **Instance of Solution:** Implemented high-performance range detection. By pre-allocating a fixed results buffer at initialization and filtering targets via LayerMasks, player proximity is evaluated efficiently on the main thread without triggering runtime garbage collection or memory allocations.

### 5. Centralized State & UI Management
* Utilized a centralized manager pattern to coordinate game states, pause menus, and input context switching (seamlessly shifting inputs between active combat and UI navigation maps).

---

## 🛠️ Technical Stack

* **Engine:** Unity (6000.3)
* **Language:** C#
* **Key Patterns:** Object Pooling, Component-Based Composition, Multi-threading, Manager Patterns.

---

## 🔍 For Code Reviewers (Running Locally)

If you are a developer looking to inspect in the Unity Editor:

1. **Clone the repository:**

       git clone https://github.com/FolaAdesanya13/ShortCircuit.git

2. **Open in Unity:**
   * Open **Unity Hub**, select **Add project from disk**, and choose the cloned folder (Recommended: **Unity 6000.3**).
---

## 👤 Author

* **Fola Adesanya** — Gameplay Programming & Systems Architecture
  * Email: folaadesanya13@gmail.com
  * Phone number: 443-514-9221
  * LinkedIn: https://www.linkedin.com/in/fola-adesanya/
