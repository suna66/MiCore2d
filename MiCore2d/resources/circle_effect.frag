#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform vec2 r;
uniform float radius;
uniform vec2 centor;
uniform float order;

void main()
{
  //vec2 centor = r / 2.0;
  vec2 point = centor + r / 2.0;

  vec2 p = (texCoord * r) - point;

  float len = length(p);
  float alpha = len/radius;
  if (alpha < 0.0) {
    alpha = 0.0;
  }
  if (alpha > 1.0) {
    alpha = 1.0;
  }
  if (order >= 0.0) {
    outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), alpha);
  } else {
    outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), 1.0 - alpha);
  }
}
