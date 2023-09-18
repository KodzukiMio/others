function EstimateTokenLength(input) {
  let tokenLength = 0;
  for (let i = 0; i < input.length; i++) {
    const charCode = input.charCodeAt(i);
    if (charCode < 128) {
      // ASCII character
      if (charCode <= 122 && charCode >= 65) {
        // a-Z
        tokenLength += 0.25;
      } else {
        tokenLength += 0.5;
      }
    } else {
      tokenLength += 1.5; // Unicode character
    }
  }
  return tokenLength;
}
function RWKVsubmit(
  message_,
  IP = "http://127.0.0.1:8000/v1/chat/completions",
  max_token = 8100,
  temperature_ = 1,
  top_p_ = 0.3,
  presence_penalty_ = 0,
  frequency_penalty_ = 0.5
) {
  return fetch(IP, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      'messages': message_,
      'model': "rwkv",
      'stream': false,
      'max_tokens': max_token,
      'temperature': temperature_,
      'top_p': top_p_,
      'presence_penalty': presence_penalty_,
      'frequency_penalty': frequency_penalty_,
    }),
  });
}
var Chathistory = [];
function Generate(
  IP = "http://127.0.0.1:8000/v1/chat/completions",
  max_token = 8100,
  temperature_ = 1,
  top_p_ = 0.3,
  presence_penalty_ = 0,
  frequency_penalty_ = 0.5
) {
  var subarray = [];

  var tokencounter = EstimateTokenLength(
    Chathistory[0].content + Chathistory[0].role
  );

  for (var i = Chathistory.length; i > 1; i--) {
    tokencounter += EstimateTokenLength(
      Chathistory[i].content + Chathistory[i].role
    );
    if (tokencounter >= max_token) {
      return;
    }
    subarray.unshift(Chathistory[i]);
  }
  subarray.unshift(Chathistory[0]);
  console.log(subarray);
  RWKVsubmit(
    subarray,
    IP,
    max_token,
    temperature_,
    top_p_,
    presence_penalty_,
    frequency_penalty_
  )
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
    })
    .catch((error) => {
      console.error(error);
    });
}
function Submit(message, role = "user") {
  var Role = role;
  if (!Chathistory.length) {
    Role = "system";
  }
  Chathistory.push({ role: Role, content: message });
  Generate();
}
