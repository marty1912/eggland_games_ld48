shader_type canvas_item;
render_mode unshaded;

uniform float red = 0.5;
uniform float green= 0.5;

void fragment(){
  COLOR = texture(TEXTURE,UV);
  COLOR.g += green;
}