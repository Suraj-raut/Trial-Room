from PIL import Image

def convertImg() :
    img = Image.open("Assets/Images/pngtree-brush-circle-creative-brush-effect-png-image_6020152.jpg")
    img = img.convert("RGBA")

    datas = img.getdata()

    newData = []

    for item in datas:
        if item[0] == 255 and item[1] == 255 and item[2] == 255:
            newData.append((255, 255, 255, 0))
        else:
            newData.append(item)

    img.putdata(newData)
    img.save("Assets/Images/NewBG", "PNG")

    convertImg()        
