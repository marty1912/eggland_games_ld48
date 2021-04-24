shader_type canvas_item;

uniform float red = 0.5;
uniform float green= 0.5;
uniform float blue= 0.5;

void fragment(){
  COLOR = texture(TEXTURE,UV);
  COLOR.b = blue;
}