# FOSS Skeuomorphic VR Desktop Environment
## Overview
This project explores a novel approach to Virtual Reality (VR) user interface design, moving beyond traditional 2D menus and panels. It presents a skeuomorphic, volumetric UI where interactive elements are represented as tangible, three-dimensional objects. The goal is to create an intuitive and spatial user experience by translating familiar real-world interactions—like handling physical objects—into a VR environment.
### Inspired by the structured, grid-based layouts of modern mobile operating systems like iOS, this system defines a predetermined 3D space where each UI element, or "item," is a distinct 3D volume that acts as a collider.
##  Key Features
- Skeuomorphic Design: UI elements are designed to mimic real-world objects, making the interface immediately understandable and approachable for new users.
- Volumetric UI: The entire user interface exists as a collection of 3D objects, fully integrated into the virtual space rather than being confined to a flat screen.
- Structured Layout: Items fit within a pre-defined grid, ensuring a clean and organized layout similar to a home screen with apps and widgets.
### Dynamic Interactions: The system supports a range of physical, intuitive actions for interacting with UI elements:
- Tap Action: A quick, single interaction for selection or activation.
- Press-and-Hold Action: A prolonged interaction to reveal contextual menus or secondary options.
- Grab Action: Allows users to physically pick up and manipulate elements.
- Dynamic Repositioning: When enabled, users can move and rearrange UI items within the designated 3D space, personalizing their virtual environment.
## Core Concepts
The user interface is built on three fundamental building blocks that define its structure and behavior:
- Spatial Elements: These are the individual, interactive UI components, such as a button, a display, or a tool. Each spatial element is a distinct 3D mesh with a collider that represents a specific function or piece of information.
- Spatial Organizers: These are the fixed, predetermined 3D volumes that house and arrange the spatial elements. They act as the container or canvas for the UI, providing a structured grid-like layout for all interactive items.
- Interaction Zone: This is the defined space where the user's hand or controller can perform the various actions (Tap, Press-and-Hold, Grab) on the spatial elements. It ensures that interactions are recognized and processed correctly within the UI's operational area.
## Built With
This project is developed using the Unity game engine and the Unity XR Interaction Toolkit. The UI system leverages and builds upon the core functionality of the toolkit to achieve its specific behaviors:
- Sockets: Used within Spatial Organizers to hold Spatial Elements in their predetermined positions, providing a clean, grid-based layout.
- Grab and Simple Interactors: These are the foundation for the various user interactions, enabling the Tap, Press-and-Hold, and Grab actions on the Spatial Elements.

The project extends the default behavior of these scripts to create a more integrated and customized volumetric user experience.

## Getting Started
not filled in at this time
Clone the repository:
not filled in at this time

## Contribution
We welcome contributions from the community! Feel free to open an issue or submit a pull request if you have ideas, bug fixes, or new features.

## License
Gonna be like apache or something
