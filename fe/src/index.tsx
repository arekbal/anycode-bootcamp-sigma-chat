import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

import * as signalR from "@microsoft/signalr";

async function main() {

  const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5000/chat")
  .configureLogging(signalR.LogLevel.Debug)
  .build();

  connection.on("messagereceived", data => {
    console.log(data);
  });

  connection.start()
    .then(() => connection.invoke("NewMessage", "Hello", "Jimmy!!!"))
    .catch(console.error);


  const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
  );

  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>
  );
}

main().catch(err => {
  console.error(err);
  process.exit(1)
})
