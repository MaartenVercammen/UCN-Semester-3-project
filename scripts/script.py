import json
import urllib
import requests
import uuid

def main():
    data = readFromFile()
    getData(data)

def readFromFile():
    with open('api.json', 'r', encoding='utf-8') as json_file:
        data = json.load(json_file)
    return data

def  getData(data):
    with open('recipes.sql', 'w', encoding='utf-8') as sqlFile:
        for item in data["recipes"]:
            photoStr = item["Photo"]
            strRemove = "jpg ("
            try:
                sqlString = (f'insert into recipe values ("{str(uuid.uuid4())}"  ,  "{item["Name"]}" , "{item["Notes"]}" , "00000000-0000-0000-0000-000000000000",  "{photoStr[photoStr.index(strRemove) + len(strRemove):][:-1]}", {item["Prep Time"]}, 4)' + '\n').replace("'", "")
                sqlFile.write(sqlString.replace('"', "'"))
            except ValueError:
                pass


if __name__ == "__main__":
    main()