function saveData()
{
    const data = { title: "New Title", url: "https://intranet/portal" };
window.chrome.webview.postMessage({ action: "saveConfig", payload: data });
document.getElementById("status").innerText = "Sent config to native!";
}
