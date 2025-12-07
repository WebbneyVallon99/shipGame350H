# This repository was made for the group work of Bilal Burton and Webbney Vallon

**Welcome to the Team Fish Project.**

This is the Link for our group proposals https://docs.google.com/document/d/1gbGMd_yNIB7oo7wnIZczWsmQ4QtWhfZgKr7yDzWwIAc/edit?tab=t.o0grj4whhzp1#heading=h.uazfnw3dfz9z

Our game is called **"Fishy Ship"**


# 🎮 Fishy Ship - Functional Requirements

## 🚢 **Ship Requirements**
- 📋 Allow player to move ship **left/right** within scene boundaries
- 📋 Prevent ship from leaving scene **horizontally or vertically**
- 📋 Allow ship to perform a **jump**
- 📋 Track ship's **hit points**, starting at **3 HP**
- 📋 Reduce ship's HP by **1** when colliding with obstacles
- 📋 **End game** if ship's HP reaches **0**

## 🐟 **Fish Requirements**
- 📋 Spawn fish from the **right side** of screen
- 📋 Launch fish in **randomized arc-shaped trajectories** toward left side
- 📋 Despawn fish immediately if they **hit the water**
- 📋 Assign **different point values** to different fish types
- 📋 Add points to player's score when fish are **successfully caught**

## 🏝️ **Stage Requirements**
- 📋 Automatically **scroll background** to simulate forward motion
- 📋 Automatically **scroll water** to match stage movement

## ⚠️ **Buoy Obstacle Requirements**
- 📋 Spawn buoy obstacles in **one of three possible heights**
- 📋 Reduce ship's HP by **1** upon collision with buoy
- 📋 Remove buoy after it damages ship

## ✨ **Power-up Requirements**
- 📋 Spawn power-ups collectible by player
- 📋 **Seagull power-up**: Grant **double-jump ability**
- 📋 **Wood plank power-up**: Restore **1 HP**
- 📋 **Wind power-up**: Temporarily **increase ship speed**
- 📋 **Treasure chest power-up**: Grant **temporary invincibility** + double jump + speed boost
- 📋 Display **visual/mechanical indication** when power-up is active
- 📋 Automatically **remove power-up effects** after duration expires (except permanent ones like heal)

## 📈 **Level Progress Requirements**
- 📋 Check if player reaches **required point threshold** to complete level
- 📋 **Transition to next level** when score threshold is met
- 📋 **Increase game speed/difficulty** for each subsequent level

## 🏁 **Win and Loss Requirements**
- 📋 Trigger **"win" state** if player reaches level point requirement
- 📋 Trigger **"loss" state** if player's HP reaches **0**
- 📋 Display **appropriate end-of-game feedback** for win/loss

# 📊 Weekly Progress Report - Nov 29


## 👥 **Team Celestials**

### **Team Members & Contributions:**

#### **Bilal Burton**
**✅ Tasks Completed:**
- Revised and defined class connections in UML diagram
- Created new UML diagram based on updated project structure

#### **Webbney Vallon**
**✅ Tasks Completed:**
- Reevaluated current data structures applications in project
- Researched and compared various data structures to validate intended outcomes

#### **Collaborative Work (Bilal & Webbney)**
**✅ Joint Tasks Completed:**
- Further detailed gameplay deliverables and executed corresponding tasks
- Developed structured workflow for collaborative project management

## 📈 **Project Progress**

### **🔄 GitHub Activity**
- **Updated and improved repo README** ✅
- Enhanced documentation for better clarity and structure

---

## 🎯 **Weekly Accomplishments**
**What did you accomplish this week?**
We revised and corrected past deliverables in order to create a more smooth and seamless workflow moving forwards.

---

## 🚧 **Challenges Faced**

**What challenges did you face?**
Our biggest challenge was getting in contact with each other, since we were both busy over the week and the holiday weekend. This set us back since we had information to share with each other regarding the direction of our project. Such as the distribution of deliverables across team members.

---

## ✅ **Resolution & Action Plan**

**How did you resolve them (or what is your plan to resolve them)?**
Today we had a stand up and addressed our availability and developed a routine of doing 2 virtual stand-up meetings a week and doing 2 collaborative work sessions a week on campus.

### **Implemented Solutions:**
1. **✅ Scheduled Regular Stand-ups**
   - 2 virtual stand-up meetings per week
   - 2 collaborative work sessions on campus

2. **✅ Improved Communication Channels**
   - Established consistent check-in times
   - Created shared availability calendar

3. **✅ Workflow Optimization**
   - Clear task distribution system
   - Defined collaborative work protocols

# 📊 Weekly Progress Report - Dec 6


## 👥 **Team Celestials**

### **Team Members & Contributions:**

### **Bilal Burton - Tasks Completed**
- ✅ Create background scroller
- ✅ Create Fish Spawner
- ✅ Make Fish Spawn

### **Webbney Vallon - Tasks Completed**
- ✅ Create game manager
- ✅ Make ship controllable by user
- ✅ Create buoy hazard spawner
- ✅ Create collision interaction between buoy and ship

---

## 📈 **GitHub Activity Summary**

### **Recent Commits:**
- **bouy, ship, collision feature set** · `WebbneyVallon99/shipGame350H@9efd0dd`
- **Implement FishSpawner and FishQuirks classes** · `WebbneyVallon99/shipGame350H@3d85ccc`
- **Created BackgroundScroller functionality** · `WebbneyVallon99/shipGame350H@befc221`
- **Ship moving functionality complete** · `WebbneyVallon99/shipGame350H@3471d88`

### **Key Achievement:**
✅ **Successfully merged all work onto one branch and resolved all merge conflicts**

---

## 🎯 **Weekly Accomplishments**

### **Ship & Controls**
- ✅ Ship controlled by user via **A, D, and Space keys**
- ✅ Full horizontal movement with limited vertical movement
- ✅ Ship cannot leave game window boundaries
- ✅ Ship collides with horizontal borders, vertical borders, and buoys

### **Buoy System**
- ✅ Buoys despawn upon collision with ship or game borders
- ✅ Ship only takes damage from buoy collisions (via interfacing)
- ✅ Buoys spawn in random orientations (1, 2, or 3)
- ✅ Buoy spawning stops when ship HP reaches zero

### **Game Management**
- ✅ Ship movement only when game manager reads game as active
- ✅ Game ends when ship's HP reaches zero

### **Visual & Environment**
- ✅ Background scrolls automatically during gameplay
- ✅ Fish spawn randomly with different sizes and point values
- ✅ Fish enter screen from multiple directions and despawn when off-screen

### **Gameplay Mechanics**
- ✅ Fish can be collected by ship for points
- ✅ Collectible interface implemented for future item expansion

---

## 🚧 **Challenges Faced**

### **Visual Issues**
- ❌ Background and fish are **blurry and not scaled properly**

### **Fish Behavior Problems**
- ❌ Fish only move in **one wave pattern**
- ❌ Fish don't come from **out of the water** or **left/right sides**
- ❌ Fish move at **same speed as background** (no varying speeds/angles)
- ❌ Fish have **no gravity or collision physics**

### **Ship Mechanics Issues**
- ❌ Ship can **jump multiple times** (unintended)
- ❌ Ship feels **floaty** (unbalanced jump force, gravity, move speed values)

### **Spawn System Problems**
- ❌ Buoys do **not consistently spawn** in random intervals (1, 2, 3)

### **Game Progression Issues**
- ❌ Only **one game ending condition** (HP reaching zero)

---

## ✅ **Resolution Plan**

### **Weekend Development Focus:**
1. **Visual Fixes**
   - Adjust background and fish scaling
   - Fix blurriness in sprite rendering

2. **Fish System Overhaul**
   - Implement varied movement patterns
   - Add gravity and collision physics
   - Create multiple spawn directions (water surface, sides)
   - Implement speed and angle variations

3. **Ship Mechanics Tuning**
   - Fix multiple jump bug
   - Balance jump force, gravity, and movement values
   - Improve "feel" of ship controls

4. **Spawn System Improvements**
   - Fix buoy random interval spawning
   - Ensure consistent 1, 2, 3 orientation distribution

5. **Game Progression**
   - Add additional win/loss conditions
   - Implement level progression system


### 👥 Team Collaboration

**How did your team collaborate this week?**

- Met **4 times total**:  
  - **Two Discord meetings** — Monday and Saturday  
  - **Two in-person sessions** — Tuesday and Thursday  
- Worked collaboratively on **UML refinement**, ensuring class relationships and responsibilities were clearly defined.
- Improved **task delegation**, making workflow more efficient and organized.
- Shared progress through **video demos** to showcase updated features.
- Successfully **merged all major features** into one unified project, overcoming previous integration challenges.

---

### 🗓️ Plans for Next Week

**Tasks to complete next week:**

**Webbney:**
- Continue refining **ship mechanics** and enhance interactions with buoys.
- Create **power-ups** that can be collected and used with the ship.

**Bilal:**
- Improve **background scrolling** behavior and advance **fish spawning logic**, including varied movement and patterns.
- Make fish **collectible** and track score to refine the gameplay loop and end conditions.
- Incorporate a **level manager** to build additional levels and increase difficulty.

**Both:**
- Begin integrating **level progression** and early gameplay structure.

**Challenges to be faced:**

- Ensuring all new systems integrate smoothly with the existing merged codebase.  
- Coordinating asset placement and pacing as more features become active simultaneously.  
- Designing and implementing a **boss battle** at the end of the final level.

---

### 📝 Additional Notes

- Current merged build includes working **background scroller**, **buoy hazards**, **fish spawner**, and **ship movement/jumping** mechanics.
- Work will no longer be done on separate branches; development will proceed on a unified workflow on the main branch.
 instead, just on the main branch. We will communicate when we push code.

