shader_type canvas_item;
render_mode unshaded;
// this shader was copied from  https://godotshaders.com/shader/vhs-and-crt-monitor-effect/ and adapted.

uniform float dissolve_value =0.6;//: hint_range(0,1);
uniform float red = 0.5;
uniform float green= 0.5;
uniform float blue= 0.5;


// Used by the noise functin to generate a pseudo random value between 0.0 and 1.0
vec2 random(vec2 uv){
    uv = vec2( dot(uv, vec2(127.1,311.7) ),
               dot(uv, vec2(269.5,183.3) ) );
    return -1.0 + 2.0 * fract(sin(uv) * 43758.5453123);
}

// Generate a Perlin noise used by the distortion effects
float noise(vec2 uv) {
    vec2 uv_index = floor(uv);
    vec2 uv_fract = fract(uv);

    vec2 blur = smoothstep(0.0, 1.0, uv_fract);

    return mix( mix( dot( random(uv_index + vec2(0.0,0.0) ), uv_fract - vec2(0.0,0.0) ),
                     dot( random(uv_index + vec2(1.0,0.0) ), uv_fract - vec2(1.0,0.0) ), blur.x),
                mix( dot( random(uv_index + vec2(0.0,1.0) ), uv_fract - vec2(0.0,1.0) ),
                     dot( random(uv_index + vec2(1.0,1.0) ), uv_fract - vec2(1.0,1.0) ), blur.x), blur.y) * 0.5 + 0.5;
}


void fragment(){

// just some playing around..
  //vec2 uv = random(UV);
  vec4 main_text = texture(TEXTURE,UV);
  vec2 uv = UV;
  uv.x += TIME;
  main_text.a *= floor(dissolve_value+noise(uv*1000f));

  COLOR = main_text;
}