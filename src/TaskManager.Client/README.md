# Task Manager - Frontend Setup

Instructions for running the Task Manager frontend application locally.

## Prerequisites

Before running the frontend, ensure you have the following installed:

- **Node.js (Required):** - [Node.js Installation Guide](https://nodejs.org/en/download/) (npm is included with Node.js)

#### Node Version Manager (nvm):

If you need to manage multiple Node.js versions, nvm is highly recommended.

- [nvm Installation Guide](https://github.com/nvm-sh/nvm#installing-and-updating) (for macOS/Linux)
- [nvm-windows Installation Guide](https://github.com/coreybutler/nvm-windows) (for Windows)
  Markdown

## `.env` File Configuration

Before running the frontend, you need to create a `.env` file in the root of the `/src/TaskManager.Client/` directory with the backend URL.

1.  **Create the `.env` file:**

    - In the root of the `/src/TaskManager.Client/` directory, create a new file named `.env`.

2.  **Add the backend URL:**

    - Add the following line to the `.env` file:

    ```
    VITE_BACKEND_DOMAIN="https://localhost:7226"
    ```

    - If your backend is running on a different port, adjust the URL accordingly.

## Running the Frontend Locally

1.  **Navigate to the Frontend Directory:**

    - Open your terminal or command prompt and navigate to the `/src/TaskManager.Client/` directory of your project.

    ```bash
    cd src/TaskManager.Client/
    ```

2.  **Install Dependencies:**

    - Run the following command to install the necessary npm packages:

    ```bash
    npm install
    ```

3.  **Start the Development Server:**

    - Run the following command to start the development server. This will compile the frontend and open it in your default web browser.

    ```bash
    npm run dev
    ```

    - If the browser does not open automatically, look at your terminal output. It will show the local address where the app is running. Usually it is `http://localhost:5173/`.

4.  **Access the Application:**
    - Open your web browser and navigate to the URL provided in the terminal output (usually `http://localhost:5173/`).
