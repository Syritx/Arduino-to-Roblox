from flask import Flask
import json

app = Flask(__name__)
app.config['DEBUG'] = True

@app.route('/', methods=['GET'])
def home():

    file = open('format.json')
    dat = ''
    for line in file:
        dat += line

    dat = json.dumps(json.loads(dat), indent=4)
    return dat

app.run()