from flask import Flask, request, jsonify
import tiktoken
from flask_cors import CORS

app = Flask(__name__)
CORS(app)
encoding = tiktoken.get_encoding("cl100k_base")

def num_tokens_from_string(string: str) -> int:
    return len(encoding.encode(string))

@app.route('/tokens', methods=['POST'])
def tokens():
    data = request.get_json()
    conversation = data.get('conversation')
    max_tokens = data.get('maxTokens')

    # 计算每句话的token数
    new_conversation=[]
    token_counts = [num_tokens_from_string(sentence['content']) for sentence in conversation]

    #检查
    if isinstance(conversation, list) and len(conversation) > 0:
        new_conversation = [conversation[0]]
    else:
        return jsonify({'conversation': [{'role':'system', 'content': ''}]})

    # 保证总token数不超过maxToken
    total_tokens = token_counts[0]
    for i in range(len(conversation) - 1, 0, -1):
        if total_tokens + token_counts[i] <= max_tokens:
            total_tokens += token_counts[i]
            new_conversation.insert(1, conversation[i])
        else:
            break
    print(total_tokens);#token计数            
    return jsonify({'conversation': new_conversation})

if __name__ == "__main__":
    app.run(port=5000)
