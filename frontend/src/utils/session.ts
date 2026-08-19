const SESSION_ID_KEY = "casefile_session_id";

// Function to get the session id from localstorage
// or create a new id and store it into the localstorage.
export function getSessionId(): string {
  let sessionId = localStorage.getItem(SESSION_ID_KEY);

  if (!sessionId) {
    sessionId = crypto.randomUUID();
    localStorage.setItem(SESSION_ID_KEY, sessionId);
  }

  return sessionId;
}
